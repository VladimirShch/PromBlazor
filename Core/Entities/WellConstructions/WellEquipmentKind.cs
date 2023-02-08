
using System.ComponentModel;

namespace Web_Prom.Core.Blazor.Core.Entities.WellConstructions
{
    // http://www.norma-tm.ru/sakhalin-2/sakhalin2_translation_well_design.html
    public enum WellEquipmentKind
    {
        [Description("ФонтаннаяАрматура")]
        WellheadEquipment, // ChristmasTree
        [Description("КолоннаяГоловка")]
        CasingHead,
        [Description("Пакер")]
        Packer,
        [Description("КлапанОтсекатель")]
        SafetyValve,
        [Description("Truba")]
        Pipe,
        [Description("Направление")]
        Conductor,
        [Description("Кондуктор")]
        SurfaceCasing,
        [Description("ТехКолонна")]
        IntermediateCasing,
        [Description("ЭксплуатационнаяКолонна")]
        Casing,
        [Description("ЭксплКолоннаДополнительная")]
        CasingAdditional,
        [Description("Хвостовик")]
        Liner,
        [Description("НКТ1")]
        Tubing,
        [Description("ЦЛК")]
        ConcentricTubing, // ?????!
        [Description("Фильтр")]
        Filter,       
        [Description("ЦемМост")]
        CementPlug,
        [Description("Посторонние")]
        Other,
        
        [Description("Неизвестно")]
        Unknown
    }
}
