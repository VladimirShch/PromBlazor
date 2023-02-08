using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.WellJobs;

namespace Web_Prom.Core.Blazor.DataAccess.WellJobs
{
    public class WellJobDeleter
    {
        private readonly Geolog.Contracts.IByArc _byArcService;
        private readonly UserCredentials _userCredentials;

        public WellJobDeleter(Geolog.Contracts.IByArc byArcService, UserCredentials userCredentials)
        {
            _byArcService = byArcService;
            _userCredentials = userCredentials;
        }

        public async Task Delete(WellJob wellJob)
        {
        }
    }
}
