using System;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed.Services;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Services
{
    public class SurveysCalculationService : ISurveysCalculationService
    {
        private readonly Geolog.Contracts.IIssl _isslService;
        private readonly IAdapter<WellClass.Well.IsslCs, WellSurvey> _wellSurveyAdapter;
        public SurveysCalculationService(Geolog.Contracts.IIssl isslService, IAdapter<WellClass.Well.IsslCs, WellSurvey> wellSurveyAdapter)
        {
            _isslService = isslService;
            _wellSurveyAdapter = wellSurveyAdapter;
        }
       
        public async Task<WellSurvey> CalculateReservoirPressure(string uwi, WellSurvey wellSurvey)
        {
            WellClass.Well.IsslCs wellSurveyDto = _wellSurveyAdapter.ConvertBack(wellSurvey);
            WellClass.Well.IsslCs calculatedSurveyDto = await _isslService.RaschetPplAsync(uwi, wellSurveyDto);
            WellSurvey calculatedSurvey = _wellSurveyAdapter.Convert(calculatedSurveyDto);

            return calculatedSurvey;
        }

        public async Task<WellSurvey> CalculateGasDynamicSurvey(string uwi, WellSurvey wellSurvey)
        {
            WellClass.Well.IsslCs wellSurveyDto = _wellSurveyAdapter.ConvertBack(wellSurvey);
            WellClass.Well.IsslCs calculatedSurveyDto = await _isslService.RaschetWellIsslAsync(uwi, wellSurveyDto);
            WellSurvey calculatedSurvey = _wellSurveyAdapter.Convert(calculatedSurveyDto);

            return calculatedSurvey;
        }

        public async Task<WellSurvey> CalculateLoweringDepth(string uwi, WellSurvey wellSurvey)
        {
            WellClass.Well.IsslCs wellSurveyDto = _wellSurveyAdapter.ConvertBack(wellSurvey);
            WellClass.Well wellDto = new()
            {
                UWI = uwi,
                Issl = new System.Collections.Generic.List<WellClass.Well.IsslCs> { wellSurveyDto }
            };
            if (wellDto.Issl[0]?.Glub is not null)
            {
                wellDto = await _isslService.LededkaHStvolAsync(wellDto, false);
                // рассчитаем уровень жидкости по скважине
                wellDto.Issl[0].Glub = await _isslService.CalclHliqudAsync("MockName", wellDto);
            }
            else
            {
                // TODO: сделать особое исключение на уровне наверное Application, типа Repository, но Service
                throw new Exception("Не найден глубинный замер");
            }

            WellSurvey surveyWithCalculatedDepth = _wellSurveyAdapter.Convert(wellDto.Issl[0]);
            return surveyWithCalculatedDepth;
        }

        public async Task<WellSurvey> CalculateDensityAndLiquidLevel(WellSurvey wellSurvey)
        {
            WellClass.Well.IsslCs wellSurveyDto = _wellSurveyAdapter.ConvertBack(wellSurvey);
            // Username, password, fieldId не нужен??
            WellClass.Well.IsslCs.GlubClass deepSurveyCalculatedDto = await _isslService.CalclHliqudAsync("MockName", "111", 0, wellSurveyDto.Glub, wellSurveyDto.ID);
            WellClass.Well.IsslCs calculatedSurveyDto = new() { Glub = deepSurveyCalculatedDto };
            WellSurvey calculatedWellSurvey = _wellSurveyAdapter.Convert(calculatedSurveyDto);

            return calculatedWellSurvey;
        }
       
    }
}
