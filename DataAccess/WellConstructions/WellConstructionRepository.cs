using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;

namespace Web_Prom.Core.Blazor.DataAccess.WellConstructions
{
    public class WellConstructionRepository : IWellConstructionRepository
    {
        private readonly UserCredentials _userCredentials;
        private readonly Geolog.Contracts.IByArc _byArcService;

        public WellConstructionRepository(UserCredentials userCredentials, Geolog.Contracts.IByArc byArcService)
        {
            _userCredentials = userCredentials;
            _byArcService = byArcService;
        }

        public async Task DeleteConstructionItemAsync(string fieldId, int itemId)
        {
            int fieldIdNumeric = int.Parse(fieldId);
            _ = await _byArcService.DeleteOneConstractionAsync(_userCredentials.Username, _userCredentials.Password, fieldIdNumeric, itemId);
        }
    }
}
