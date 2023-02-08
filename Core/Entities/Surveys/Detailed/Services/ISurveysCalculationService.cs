using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed.Services
{
    public interface ISurveysCalculationService
    {
        Task<WellSurvey> CalculateReservoirPressure(string uwi, WellSurvey wellSurvey);

        Task<WellSurvey> CalculateGasDynamicSurvey(string uwi, WellSurvey wellSurvey);

        Task<WellSurvey> CalculateLoweringDepth(string uwi, WellSurvey surveyModel);

        Task<WellSurvey> CalculateDensityAndLiquidLevel(WellSurvey surveyModel);
    }
}
