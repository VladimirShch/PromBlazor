using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.WellGroups
{
    public interface IWellGroupShortRepository
    {
        IEnumerable<ReferenceObject> GetWellGroups(string plantId);
        Task<IEnumerable<ReferenceObject>> GetWellGroupsAsync(string plantId);
    }
}
