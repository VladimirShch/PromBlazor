using System.Data;

namespace Web_Prom.Core.Blazor.DataAccess.WellGroups
{
    public interface IWellGroupShortRepositoryMaker
    {
        void MakeRepository(DataTable wellGroupsTable);
    }
}
