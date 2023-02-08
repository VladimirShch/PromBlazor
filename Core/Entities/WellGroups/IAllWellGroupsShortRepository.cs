using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.WellGroups
{
    public interface IAllWellGroupsShortRepository
    {
        IEnumerable<ReferenceObject> GetWellGroups();
    }
}
