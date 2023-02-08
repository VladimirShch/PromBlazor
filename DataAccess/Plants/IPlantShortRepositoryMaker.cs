
using System.Data;

namespace Web_Prom.Core.Blazor.DataAccess.Plants
{
    public interface IPlantShortRepositoryMaker
    {
        void MakeRepository(string fieldId, DataTable plantsTable);
    }
}
