using System.Collections.Generic;
using System.Data;
using Web_Prom.Core.Blazor.Core.Entities.Wells;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Short
{
    public interface IWellShortAdapter
    {
        IEnumerable<WellShort> Convert(DataSet dataSet);
    }
}
