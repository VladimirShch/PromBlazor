using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.WellConstructions
{
    // ! Пока что не удалять его! Возможно, пригодится при изменении структуры классов
    // или при непосредственном заполнении справочника оборудования из программы
    public interface IEquipmentTypeRepository
    {
        Task<IEnumerable<EquipmentType>> GetAll();
    }
}
