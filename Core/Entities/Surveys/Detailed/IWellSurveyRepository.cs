using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public interface IWellSurveyRepository
    {
        Task<WellSurvey> Get(string fieldId, string surveyId);

        Task Set(string fieldId, string wellId, WellSurvey survey);
    }
}
