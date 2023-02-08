
using System.Data;

namespace Web_Prom.Core.Blazor.DataAccess.Horizons
{
    public interface IHorizonRepositoryMaker
    {
        void MakeRepository(DataTable horizonsTable);
    }
}
