using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed.Services;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Services
{
    public class SurveysDeletionService : ISurveysDeletionService
    {
        private readonly Geolog.Contracts.IIssl _isslService;
        private readonly UserCredentials _userCredentials;
        public SurveysDeletionService(UserCredentials userCredentials, Geolog.Contracts.IIssl isslService)
        {
            _userCredentials = userCredentials;
            _isslService = isslService;
        }

        public async Task DeleteDeepSurveyStation(int fieldId, int stationId)
        {
            _ = await _isslService.DeleteOneGlubZamerAsync(_userCredentials.Username, _userCredentials.Password, fieldId, stationId);
        }

        public async Task DeleteGasDynamicRegime(int fieldId, int gasDynamicRegimeId)
        {
            _ = await _isslService.DeleteOneRejimAsync(_userCredentials.Username, _userCredentials.Password, fieldId, gasDynamicRegimeId);
        }

        public async Task DeleteGeophysicalContact(int fieldId, int geophysicalContactId)
        {
            _ = await _isslService.DeleteOneContactAsync(_userCredentials.Username, _userCredentials.Password, fieldId, geophysicalContactId);
        }

        public async Task DeleteInflowInterval(int fieldId, int inflowIntervalId)
        {
            _ = await _isslService.DeleteOnePritokAsync(_userCredentials.Username, _userCredentials.Password, fieldId, inflowIntervalId);
        }
    }
}
