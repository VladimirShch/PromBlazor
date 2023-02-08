using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures;

namespace Web_Prom.Core.Blazor.DataAccess.ReservoirPressures
{
    public class ReservoirPressureInformationAdapter : IReservoirPressureInformationAdapter
    {
        public ReservoirPressureInformation Convert(WellClass.Well wellDto)
        {
            var reservoirPressureInformation = new ReservoirPressureInformation
            {
                Uwi = wellDto.UWI,
                PressureCalculationMethod = GetCalculationMethod(wellDto.Charac?.TrendType),
                CoefficientA = wellDto.Charac?.TrendType?.kA ?? 0, // Внимание, почему 0??? это же коэффициент - в теории может быть деление на 0, ну или умножение
                CoefficientB = wellDto.Charac?.TrendType?.kB ?? 0,
                ABDate = wellDto.Charac?.TrendType?.ABDate ?? new DateTime(1900, 01, 01),
                ModelPressureCoefficient = wellDto.Charac.TrendType.kM_dP,
                ModelAngleCoefficient = wellDto.Charac.TrendType.kM_Angle,
                ModelAngleDatePoint = wellDto.Charac.TrendType.kM_AnglePoint,
                ModelScale = wellDto.Charac.TrendType.kM_Scale,
                ModelScaleLine = wellDto.Charac.TrendType.kM_ScaleLine
            };

            if (wellDto.Issl is not null)
            {
                reservoirPressureInformation.SurveysData = wellDto.Issl.Where(t => t is not null && t.Stat is not null)
                    .OrderBy(t => t.Finish).Select(ConvertDto).Where(t => t.Value > 0f).ToList();
            }

            if (wellDto.Raport is not null)
            {
                reservoirPressureInformation.ReportData = wellDto.Raport
                    .Where(t => t is not null && t.Ppl_Priv > 0f && t.kN == 0f).Select(t => ConvertDto(t)).ToList();

                reservoirPressureInformation.ModelData = wellDto.Raport
                    .Where(t => t is not null && t.Ppl_Priv > 0f && t.kN == 1f).Select(t => ConvertDto(t, true)).ToList();
            }

            return reservoirPressureInformation;
        }

        public Geolog.Contracts.IGrafic.SaveIsslsRequest ConvertBack(ReservoirPressureInformation reservoirPressureInformation)
        {
            var saveIsslDto = new Geolog.Contracts.IGrafic.SaveIsslsRequest
            {
                // Вбить UserName,
                UWI = reservoirPressureInformation.Uwi,
                IsslsID = reservoirPressureInformation.SurveysData.Select(t => t.Id).ToArray(),
                OffIssl = reservoirPressureInformation.SurveysData.Select(t => !t.Enable).ToArray(),
                ModelID = 0,//!!!!((WellClass.EclModel)ModelList.SelectedItem).ID,
                ModelDates = reservoirPressureInformation.ModelData.Select(t => t.Date).ToArray(),
                ModelOff = reservoirPressureInformation.ModelData.Select(t => !t.Enable).ToArray(),
                Priv = new ppl_Calculator.PprivType
                {
                    kA = reservoirPressureInformation.CoefficientA,
                    kB = reservoirPressureInformation.CoefficientB,
                    ABDate = reservoirPressureInformation.ABDate,
                    kM_dP = reservoirPressureInformation.ModelPressureCoefficient,
                    kM_Angle = reservoirPressureInformation.ModelAngleCoefficient,
                    kM_AnglePoint = reservoirPressureInformation.ModelAngleDatePoint,
                    kM_Scale = reservoirPressureInformation.ModelScale,
                    kM_ScaleLine = reservoirPressureInformation.ModelScaleLine,
                    UseAB = reservoirPressureInformation.PressureCalculationMethod == PressureCalculationMethod.Manual,
                    UseIssl = reservoirPressureInformation.PressureCalculationMethod == PressureCalculationMethod.Measurements,
                    UseModel = reservoirPressureInformation.PressureCalculationMethod == PressureCalculationMethod.Model || reservoirPressureInformation.PressureCalculationMethod == PressureCalculationMethod.ModelWithCoefficient,
                    Use_kM = reservoirPressureInformation.PressureCalculationMethod == PressureCalculationMethod.ModelWithCoefficient
                }
            };

            return saveIsslDto;
        }

        private PressurePoint ConvertDto(WellClass.Well.IsslCs surveyDto)
        {
            var pressureItem = new PressurePoint(0d, surveyDto.Finish, !surveyDto.Stat.OFF)
            {
                Id = surveyDto.ID,
                SurveyType = surveyDto.Type
            };

            if (surveyDto.Stat.Ppl_plosk > 0f)
            {
                pressureItem.Source = PressureSourceType.DatumPlane;
                pressureItem.Value = surveyDto.Stat.Ppl_plosk;
            }
            else if (surveyDto.Stat.Ppl_perf > 0f)
            {
                pressureItem.Source = PressureSourceType.MiddlePerforationPoint;
                pressureItem.Value = surveyDto.Stat.Ppl_perf;
            }
            else if (surveyDto.Stat.Pglib > 0f)
            {
                pressureItem.Source = PressureSourceType.DeepSurvey;
                pressureItem.Value = surveyDto.Stat.Pglib;
            }

            return pressureItem;
        }

        private PressurePoint ConvertDto(WellClass.Well.RaportClass reportDto, bool model = false)
        {
            if (reportDto.Ppl_Priv <= 0f)
            {
                throw new Exception("Ppl_Priv must be above zero");
            }

            var pressureItem = model ? new PressurePoint(reportDto.Ppl_Priv, reportDto.Dates, !Conversions.ToBoolean(reportDto.WorkTime))
                : new PressurePoint(reportDto.Ppl_Priv, reportDto.Dates.AddMonths(-1).AddDays(14d), true);

            pressureItem.Source = PressureSourceType.DatumPlane;

            return pressureItem;
        }

        private PressureCalculationMethod GetCalculationMethod(ppl_Calculator.PprivType? trendTypeDto)
        {
            if(trendTypeDto is null)
            {
                return PressureCalculationMethod.None;
            }
            if (trendTypeDto.UseAB)
            {
                return PressureCalculationMethod.Manual;
            }
            if (trendTypeDto.UseIssl)
            {
                return PressureCalculationMethod.Measurements;
            }
            if (trendTypeDto.UseModel)
            {
                return trendTypeDto.Use_kM ? PressureCalculationMethod.ModelWithCoefficient : PressureCalculationMethod.Model;
            }

            return PressureCalculationMethod.None;
        }
    }
}
