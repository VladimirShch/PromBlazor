using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Enums;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells
{
    // Вероятно, это всё неправильно. WellShort понятия в предметной области нет, это скорее чисто техническая конструкция, чтобы не тащить все скважины, а отобразить легковесно список без подробностей.
    // Максимум, это уровень приложения, а то и интерфейса или отображения
    // Возможно, условия выбора скважин следует задавать в репозитории, а не здесь добавлять соответствующие свойства
    public class WellShort
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string PlantId { get; set; }

        public WellType Type { get; set; }

        public WellState State { get; set; }

        public WellShape Shape { get; set; } 

        public bool OnBalance { get; set; }

        public List<int> PerforationObjects { get; set; } = new();

        public List<WellShort> ChildWellbores { get; set; } = new List<WellShort>();
    }
}
