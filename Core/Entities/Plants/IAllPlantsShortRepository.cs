using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.Plants
{
    public interface IAllPlantsShortRepository
    {
        IEnumerable<ReferenceObject> GetPlants();
    }
}
