using System;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Enums;

namespace Web_Prom.Core.Blazor.Core.Entities.WellTypesHistory
{
    public class WellTypeChange
    {
        public int Id { get; set; }
        public WellType WellType { get; set; }
        public DateTime Date { get; set; }
    }
}
