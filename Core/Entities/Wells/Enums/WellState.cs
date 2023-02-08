
using System.ComponentModel;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells.Enums
{
    // TODO : вероятно, следует избавиться от хардкода состояний(это касается и прочих перечислений) на клиенте, а получать их из базы
    public enum WellState
    {
        [Description("Действующая")]
        Active,

        [Description("Ожидающая капремонта")]
        WaitingWorkingOver,

        [Description("Ожидающая подключения (принятая)")]
        WaitingPluggingAccepted,

        [Description("Ожидающая освоения (принятая)")]
        WaitingInitializingAccepted, // Ожидающая освоения, по поводу перевода термина освоение есть споры - от initial start up до completion

        [Description("Ожидающая подключения (непринятая)")]
        WaitingPluggingInaccepted,

        [Description("В освоении")]
        Initializing,

        [Description("Ожидающая освоения (непринятая)")]
        WaitingInitializingInaccepted,

        [Description("В бурении")]
        Drilling,

        [Description("Проектная")]
        Project,

        [Description("В консервации")]
        Conserved,

        [Description("Бездействующая")]
        Inactive,

        [Description("В ремонте")]
        WorkingOver, //Overhaul, // капремонт?

        [Description("Ожидающая ликвидации")]
        WaitingAbandoning,

        [Description("Ликвидированная")]
        Abandoned,

        [Description("Нет данных")]
        Unknown
    }
}
