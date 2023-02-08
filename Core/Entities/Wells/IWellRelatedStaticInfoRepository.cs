using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells
{
    public interface IWellRelatedStaticInfoRepository
    {
        IEnumerable<ReferenceObject> GetPlants();
        IEnumerable<ReferenceObject> GetWellGroups();
        IEnumerable<EntityHeader> GetDevelopmentObjects();
        IEnumerable<EntityHeader> GetHorizons();
        IEnumerable<EntityHeader> GetReservoirs();
        IEnumerable<EntityHeader> GetPerforationObjects();
    }
}
