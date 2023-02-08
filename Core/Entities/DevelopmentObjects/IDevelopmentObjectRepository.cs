using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.DevelopmentObjects
{
    public interface IDevelopmentObjectRepository
    {
       IEnumerable<EntityHeader> GetObjects();
    }
}
