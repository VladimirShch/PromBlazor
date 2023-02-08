using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.Horizons
{
    public interface IHorizonRepository
    {
        IEnumerable<EntityHeader> GetHorizons();
    }
}
