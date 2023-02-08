
using System.ComponentModel;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells.Enums
{
    public enum WellType
    {
        [Description("Эксплуатационная")]
        Production,

        [Description("Наблюдательная перфорированная")]
        ObservationPerforated,

        [Description("Наблюдательная глухая")]
        ObservationNotPerforated,

        [Description("Поглощающая")]
        Absorbing,

        [Description("Пьезометрическая")]
        Piezometric,

        [Description("Поисковая")]
        Appraisal,

        [Description("Разведочная")]
        Exploration,

        [Description("Мерзлотная")]
        Permafrost,

        [Description("Нет данных")]
        Unknown
    }
}
