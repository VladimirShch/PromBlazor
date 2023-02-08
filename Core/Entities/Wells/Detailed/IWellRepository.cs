using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed
{
    // Вероятно, интерфейсы репозиториев, как и ошибка репозитория, должны быть на уровне приложения, ApplicationLayer
    public interface IWellRepository
    {
        // TODO: fieldId явно лишний, сделать с этим что-нибудь. Отказаться вообще? инжектить в конструкторе?
        Task<Well> Get(string fieldId, string id);
        // TODO: fieldId явно лишний, сделать с этим что-нибудь
        Task Set(string fieldId, Well well);
        // Возвращает Id созданной скважины
        Task<string> CreateWell(string fieldId, Well well);
    }
}
