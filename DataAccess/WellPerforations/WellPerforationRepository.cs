using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.Perforations;

namespace Web_Prom.Core.Blazor.DataAccess.WellPerforations
{
    public class WellPerforationRepository : IWellPerforationRepository
    {
        private readonly UserCredentials _userCredentials;
        private readonly Geolog.Contracts.IByArc _byArcService;

        public WellPerforationRepository(UserCredentials userCredentials, Geolog.Contracts.IByArc byArcService)
        {
            _userCredentials = userCredentials;
            _byArcService = byArcService;
        }

        public async Task DeletePerforationItemAsync(string fieldId, int itemId)
        {
            int fieldIdNumeric = int.Parse(fieldId);
            _ = await _byArcService.DeleteOnePerforationAsync(_userCredentials.Username, _userCredentials.Password, fieldIdNumeric, itemId);
        }
    }
}
