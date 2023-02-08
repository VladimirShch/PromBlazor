using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;

namespace Web_Prom.Core.Blazor.Core.Entities.ReferenceInformation
{
    // TODO: Отделить репозиторий справочной информации, необходимой для справочника скважин от информации для исследований - и тогда в обоих будет GetWellSurveyTypes?
    // Или отделить справочную информацию о скважинах от справочной информации об исследованиях - и тогда GetWellSurveyTypes пойдёт туда?
    public interface IDynamicReferenceInformationRepository //IWellRelatedDynamicInfoRepository было
    {
        IEnumerable<EquipmentType> GetEquipmentTypes();

        IEnumerable<EntityHeader> GetWellJobsTypes();

        IEnumerable<EntityHeader> GetWellJobsContractors();

        IEnumerable<EntityHeader> GetWellSurveyTypes();
       
        // --------Исследования
        IEnumerable<string> GetAutomobiles();

        IEnumerable<EntityHeader> GetSurveyJobTypes();

        IEnumerable<EntityHeader> GetSurveyContractors();

        IEnumerable<WellSurveyJobResponsiblePerson> GetSurveyJobResponsiblePersons(); // TODO: протечка! см. класс
        
        IEnumerable<EntityHeader> GetReservoirPressureCalculationMethods();
        
        IEnumerable<EntityHeader> GetBottomholePressureCalculationMethods();

        IEnumerable<EntityHeader> GetInflowTypes();
        IEnumerable<EntityHeader> GetEquipmentTypesHeaders();
        IEnumerable<EntityHeader> GetWaterHydrochemicalTypes();
    }
}
