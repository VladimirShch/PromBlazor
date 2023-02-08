
using System;

namespace Web_Prom.Core.Blazor.Core.Entities.WellConstructions
{
    // TODO: вероятно, стоит сделать на уровне домена классы WellEquipment и WellPipe
    public class WellConstructionItem
    {   // Добавить тип??!!
        public int Id { get; set; }
        //public string Name { get; set; }
        public int Code { get; set; }
        //public WellEquipmentKind Type { get; set; }
        public float OuterDiameter { get; set; }
        public float InnerDiameter { get; set; }
        public float Top { get; set; }
        public float Base { get; set; }
        //public float Length { get; set; }
        public float TopAbsolute { get; set; }
        public float BaseAbsolute { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime RemovementDate { get; set; }
        public float Thickness { get; set; }
        public int Connection { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
