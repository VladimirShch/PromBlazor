
using System.ComponentModel;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells.Enums
{
    public enum WellShape
    {
        [Description("Вертикальная")]
        Vertical,

        [Description("Наклонная")]
        Inclined,

        [Description("Субгоризонтальная")]
        Subhorizontal,

        [Description("Горизонтальная")]
        Horizontal,

        [Description("Нет данных")]
        Unknown
    }
}
