
using System.Data;

namespace Web_Prom.Core.Blazor.DataAccess.DevelopmentObjects
{
    public interface IDevelopmentObjectRepositoryMaker
    {
        void MakeRepository(DataTable developmentObjectsTable);
    }
}
