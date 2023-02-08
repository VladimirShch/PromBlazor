using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.Plants
{
    public interface IPlantShortRepository
    {
        IEnumerable<ReferenceObject> GetPlants(string fieldId);
        Task<IEnumerable<ReferenceObject>> GetPlantsAsync(string fieldId);
    }
}
