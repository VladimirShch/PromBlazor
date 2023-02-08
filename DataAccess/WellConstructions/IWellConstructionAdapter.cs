using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;

namespace Web_Prom.Core.Blazor.DataAccess.WellConstructions
{
    public interface IWellConstructionAdapter
    {
        ICollection<WellConstructionItem> Convert(WellClass.Well.ConstrCs constructionDto);
        WellClass.Well.ConstrCs ConvertBack(ICollection<WellConstructionItem> constructionItems, IEnumerable<EquipmentType> equipmentTypes);
    }
}
