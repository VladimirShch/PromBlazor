using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.DataAccess.Common;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.WellConstructions
{
    // ! Пока что не удалять его! Возможно, пригодится при изменении структуры классов
    // или при непосредственном заполнении справочника оборудования из программы
    public class EquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly Geolog.Contracts.IWORK _workService;
        private readonly IAdapter<DataTable?, ICollection<EquipmentType>> _equipmentTypesAdapter;

        public EquipmentTypeRepository(Geolog.Contracts.IWORK workService, IAdapter<DataTable?, ICollection<EquipmentType>> equipmentTypesAdapter)
        {
            _workService = workService;
            _equipmentTypesAdapter = equipmentTypesAdapter;
        }

        public async Task<IEnumerable<EquipmentType>> GetAll()
        {
            DataSet allCodes = await _workService.LoadCodesAsync();
            if(allCodes is null)
            {
                throw new RepositoryException("Не удалось получить справочник оборудования");
            }
            if(allCodes.Tables["Конструкция"] is null)
            {
                throw new RepositoryException("Оборудование отсутствует в справочнике");
            }
            DataTable constructionCodes = allCodes.Tables["Конструкция"];
            
            IEnumerable<EquipmentType>  equipmentTypes = _equipmentTypesAdapter.Convert(constructionCodes);

            return equipmentTypes;
        }
    }
}
