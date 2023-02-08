using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.ReferenceInformation;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.ReferenceInformation
{
    public class DynamicReferenceInformationRepository : IDynamicReferenceInformationRepositoryAndMaker
    {
        private readonly Geolog.Contracts.IWORK _workService;
        private readonly IAdapter<DataTable?, ICollection<EquipmentType>> _equipmentTypesAdapter;

        private readonly List<EquipmentType> _equipmentTypes = new();
        private readonly List<EntityHeader> _wellJobsTypes = new();
        private readonly List<EntityHeader> _wellJobsContractors = new();
        private readonly List<EntityHeader> _wellSurveyTypes = new();
        // Для исследований - предыдущий пункт тоже - и туда, и туда
        private readonly List<string> _automobiles = new();
        private readonly List<EntityHeader> _surveyJobTypes = new();
        private readonly List<EntityHeader> _surveyContractors = new();
        private readonly List<WellSurveyJobResponsiblePerson> _surveyJobResponsiblePersons = new(); // TODO: протечка!
        private readonly List<EntityHeader> _reservoirPressureCalculationMethods = new();
        private readonly List<EntityHeader> _bottomholePressureCalculationMethods = new();
        private readonly List<EntityHeader> _inflowTypes = new();
        private readonly List<EntityHeader> _equipmentTypesHeaders = new();
        private readonly List<EntityHeader> _hydrochemicalSurveysWaterTypes = new();

        public DynamicReferenceInformationRepository(Geolog.Contracts.IWORK workService, IAdapter<DataTable?, ICollection<EquipmentType>> equipmentTypesAdapter)
        {
            _workService = workService;
            _equipmentTypesAdapter = equipmentTypesAdapter;
        }

        public async Task FillRepositoryAsync()
        {
            DataSet codesDto = await _workService.LoadCodesAsync();

            FillEquipmentTypes(codesDto.Tables["Конструкция"]);

            FillWellJobsTypes(codesDto.Tables["Работы"]);

            FillContractors(codesDto.Tables["Подрядчики"]);

            FillWellSurveyTypes(codesDto.Tables["ВидИсследований"]);
            //---Исследования
            FillAutomobils(codesDto.Tables["Automobils"]);
            
            FillSurveyJobTypes(codesDto.Tables["Работы"]);

            FillSurveyContractors(codesDto.Tables["Подрядчики"]);

            FillSurveyJobResponsiblePersons(codesDto.Tables["Исполнители"]);

            FillReservoirPressureCalculationMethods(codesDto.Tables["РасчетСтатЗаб_Давления"]);

            FillBottomholePressureCalculationMethods(codesDto.Tables["РасчетЗабойногоНаРежиме"]);
            
            FillInflowTypes(codesDto.Tables["ТипыПритока"]);

            FillEquipmentTypesHeaders(codesDto.Tables["Конструкция"]);
            FillWaterHydrochemicalTypes(codesDto.Tables["ТипВоды"]);
        }

        public IEnumerable<EquipmentType> GetEquipmentTypes()
        {
            return _equipmentTypes;
        }

        public IEnumerable<EntityHeader> GetWellJobsTypes()
        {
            return _wellJobsTypes;
        }

        public IEnumerable<EntityHeader> GetWellJobsContractors()
        {
            return _wellJobsContractors;
        }

        public IEnumerable<EntityHeader> GetWellSurveyTypes()
        {
            return _wellSurveyTypes;
        }

        public IEnumerable<string> GetAutomobiles()
        {
            return _automobiles;
        }

        public IEnumerable<EntityHeader> GetSurveyJobTypes()
        {
            return _surveyJobTypes;
        }

        public IEnumerable<EntityHeader> GetSurveyContractors()
        {
            return _surveyContractors;
        }

        // Протечка!
        public IEnumerable<WellSurveyJobResponsiblePerson> GetSurveyJobResponsiblePersons()
        {
            return _surveyJobResponsiblePersons;
        }

        public IEnumerable<EntityHeader> GetReservoirPressureCalculationMethods()
        {
            return _reservoirPressureCalculationMethods;
        }

        public IEnumerable<EntityHeader> GetBottomholePressureCalculationMethods()
        {
            return _bottomholePressureCalculationMethods;
        }

        public IEnumerable<EntityHeader> GetInflowTypes()
        {
            return _inflowTypes;
        }

        public IEnumerable<EntityHeader> GetEquipmentTypesHeaders()
        {
            return _equipmentTypesHeaders;
        }

        public IEnumerable<EntityHeader> GetWaterHydrochemicalTypes()
        {
            return _hydrochemicalSurveysWaterTypes;
        }

        private void FillEquipmentTypes(DataTable constructionCodes)
        {
            if (constructionCodes is not null)
            {
                IEnumerable<EquipmentType> equipmentTypes = _equipmentTypesAdapter.Convert(constructionCodes);
                _equipmentTypes.Clear();
                _equipmentTypes.AddRange(equipmentTypes);
            }

        }

        private void FillWellJobsTypes(DataTable allJobsTypes)
        {
            if (allJobsTypes is null)
            {
                return;
            }
            _wellJobsTypes.Clear();
            foreach (DataRow wellJobGroup in allJobsTypes.Select("GRP=900"))
            {
                _wellJobsTypes.Add(new EntityHeader
                (
                    Convert.ToInt32(wellJobGroup["ID"]),
                    wellJobGroup["Name"].ToString()
                ));

                foreach (DataRow wellJobType in allJobsTypes.Select("GRP=" + wellJobGroup["ID"]))
                {
                    _wellJobsTypes.Add(new EntityHeader
                    (
                        Convert.ToInt32(wellJobType["ID"]),
                        wellJobGroup["Name"].ToString() + "|" + wellJobType["Name"].ToString()
                    ));
                }
            }
        }

        private void FillContractors(DataTable contractors)
        {
            if (contractors is null)
            {
                return;
            }

            _wellJobsContractors.Clear();
            foreach (DataRow contractor in contractors.Rows)
            {
                _wellJobsContractors.Add(new EntityHeader(
                    Convert.ToInt32(contractor["ID"]),
                    contractor["Name"].ToString()
                    ));
            }
        }

        private void FillWellSurveyTypes(DataTable wellSurveyTypes)
        {
            if (wellSurveyTypes is null)
            {
                return;
            }
            _wellSurveyTypes.Clear();
            foreach (DataRow wellSurveyType in wellSurveyTypes.Rows)
            {
                _wellSurveyTypes.Add(new EntityHeader(
                    Convert.ToInt32(wellSurveyType["ID"]),
                    wellSurveyType["Name"].ToString()
                    ));
            }
        }

        private void FillAutomobils(DataTable automobiles)
        {
            if (automobiles is null)
            {
                return;
            }

            _automobiles.Clear();
            foreach (DataRow automobil in automobiles.Rows)
            {
                _automobiles.Add(automobil[0].ToString());
            }
        }

        private void FillSurveyJobTypes(DataTable surveyJobTypes)
        {
            if (surveyJobTypes is null)
            {
                return;
            }
            _surveyJobTypes.Clear();
            foreach (DataRow surveyJobType in surveyJobTypes.Select("GRP=" + WellClass.CODES.Works.ISSLWork, "CODE")) // 2500", "CODE")
            {
                _surveyJobTypes.Add(new EntityHeader(
                    Convert.ToInt32(surveyJobType["ID"]),
                    surveyJobType["Name"].ToString()));
            }
        }

        private void FillSurveyContractors(DataTable surveyContractors)
        {
            if (surveyContractors is null)
            {
                return;
            }
            _surveyContractors.Clear();
            foreach (DataRow surveyContractor in surveyContractors.Select("", "Name"))
            {
                _surveyContractors.Add(new EntityHeader(
                   Convert.ToInt32(surveyContractor["ID"]),
                   surveyContractor["Name"].ToString().Trim()));
            }
        }

        private void FillSurveyJobResponsiblePersons(DataTable responsiblePersons)
        {
            if (responsiblePersons is null)
            {
                return;
            }
            _surveyJobResponsiblePersons.Clear();
            foreach (DataRow responsiblePerson in responsiblePersons.Select("", "Фамилия, Имя, Отчество"))
            {
                _surveyJobResponsiblePersons.Add(new WellSurveyJobResponsiblePerson(
                  Convert.ToInt32(responsiblePerson["ID"]),
                  responsiblePerson["Фамилия"].ToString().Trim() + " " + responsiblePerson["Имя"].ToString().Trim() + " " + responsiblePerson["Отчество"].ToString().Trim(),
                  Convert.ToInt32(responsiblePerson["ID_ORG"]), 
                  responsiblePerson["Login"].ToString().Trim()
                 ));
            }
        }

        private void FillReservoirPressureCalculationMethods(DataTable calculationMethods)
        {
            if (calculationMethods is null)
            {
                return;
            }
            _reservoirPressureCalculationMethods.Clear();
            foreach (DataRow calculationMethod in calculationMethods.Rows)
            {
                _reservoirPressureCalculationMethods.Add(new EntityHeader(
                    Convert.ToInt32(calculationMethod["ID"]),
                    calculationMethod["Name"].ToString()
                    ));
            }
        }

        private void FillBottomholePressureCalculationMethods(DataTable calculationMethods)
        {
            if (calculationMethods is null)
            {
                return;
            }
            _bottomholePressureCalculationMethods.Clear();
            foreach (DataRow calculationMethod in calculationMethods.Rows)
            {
                _bottomholePressureCalculationMethods.Add(new EntityHeader(
                    Convert.ToInt32(calculationMethod["ID"]),
                    calculationMethod["Name"].ToString()
                    ));
            }
        }

        private void FillInflowTypes(DataTable inflowTypes)
        {
            if (inflowTypes is null)
            {
                return;
            }
            _inflowTypes.Clear();
            foreach (DataRow inflowType in inflowTypes.Rows)
            {
                _inflowTypes.Add(new EntityHeader(
                    Convert.ToInt32(inflowType["ID"]),
                    inflowType["ShotName"].ToString()
                    ));
            }
        }
        // Костыль
        private void FillEquipmentTypesHeaders(DataTable equipmentTypes)
        {
            if (equipmentTypes is null)
            {
                return;
            }
            _equipmentTypesHeaders.Clear();
            foreach (DataRow equipmentType in equipmentTypes.Rows)
            {
                _equipmentTypesHeaders.Add(new EntityHeader(
                    Convert.ToInt32(equipmentType["ID"]),
                    equipmentType["GrpShort"].ToString() + "-" + equipmentType["Name"].ToString()));
            }
        }

        private void FillWaterHydrochemicalTypes(DataTable waterTypes)
        {
            if (waterTypes is null)
            {
                return;
            }
            _hydrochemicalSurveysWaterTypes.Clear();
            foreach (DataRow waterType in waterTypes.Rows)
            {
                _hydrochemicalSurveysWaterTypes.Add(new EntityHeader(
                    Convert.ToInt32(waterType["ID"]),
                    waterType["ShotName"].ToString()));
            }
        }
    }
}
