using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed.Services
{
    // Немного искусственный сервис, не из предметной области, прямо говоря
    // В крайнем случае, нужен репозиторий, но тогда и получение замеров глубинки надо вынести в него
    public interface ISurveysDeletionService
    {
        Task DeleteDeepSurveyStation(int fieldId, int stationId);
        Task DeleteGasDynamicRegime(int fieldId, int regimeId);
        Task DeleteGeophysicalContact(int fieldId, int contactId);
        Task DeleteInflowInterval(int fieldId, int intervalId);
    }
}
