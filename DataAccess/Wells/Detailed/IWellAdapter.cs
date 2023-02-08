using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Detailed
{
    public interface IWellAdapter
    {
        Well Convert(WellClass.Well wellDto);
        WellClass.Well ConvertBack(string fieldId, Well well, IEnumerable<EquipmentType> equipmentTypes);
    }
}
