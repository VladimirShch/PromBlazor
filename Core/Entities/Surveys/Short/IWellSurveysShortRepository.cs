using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Short
{
    public interface IWellSurveysShortRepository
    {
        Task<IEnumerable<WellSurveyShort>> GetAll(string selectedField, string uwi);
    }
}
