using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Perforations;

namespace Web_Prom.Core.Blazor.DataAccess.WellPerforations
{
    public interface IPerforationAdapter
    {
        ICollection<Perforation> Convert(WellClass.Well.PerfCs[]? perforationsDto, IEnumerable<WellClass.Well.VskrutCl> openingsDto);

        public WellClass.Well.PerfCs[]? ConvertBack(ICollection<Perforation> itemFrom);

    }
}
