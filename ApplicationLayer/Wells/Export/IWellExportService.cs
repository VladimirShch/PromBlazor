using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Wells;

namespace Web_Prom.Core.Blazor.ApplicationLayer.Wells.Export
{
    public interface IWellExportService
    {
        Task ExportWellList(IEnumerable<WellShort> wells, string selectedFieldId);
    }
}
