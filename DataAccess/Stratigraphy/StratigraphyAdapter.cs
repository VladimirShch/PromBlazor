using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Stratigraphy;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Stratigraphy
{
    public class StratigraphyAdapter : IAdapter<List<WellClass.Well.VskrutCl>?, ICollection<DrillingIn>>
    {
        public ICollection<DrillingIn> Convert(List<WellClass.Well.VskrutCl>? drillingsInDto)
        {
            if(drillingsInDto is null)
            {
                return new List<DrillingIn>();
            }

            ICollection<DrillingIn>  result = drillingsInDto.Where(t => t is not null).Select(ConvertItem).ToList();

            return result;
        }

        public List<WellClass.Well.VskrutCl>? ConvertBack(ICollection<DrillingIn> drillingsIn)
        {
            if(drillingsIn is null)
            {
                return null;
            }

            List<WellClass.Well.VskrutCl>? drillingsInDto = drillingsIn.Where(t => t is not null).Select(ConvertItemBack).ToList();
            
            return drillingsInDto;
        }

        private DrillingIn ConvertItem(WellClass.Well.VskrutCl drilingInDto)
        {
            var drillingIn = new DrillingIn
            {
                Id = drilingInDto.ID,
                Source = drilingInDto.Source,
                Reservoir = drilingInDto.Plast,
                Top = drilingInDto.Top,
                Base = drilingInDto.Baze,
                DH = drilingInDto.dH,
                HEffective = drilingInDto.Hef,
                Remark = drilingInDto.Note,
                AbsoluteTop = drilingInDto.AOTop,
                AbsoluteBase = drilingInDto.AOBaze,
                AbsoluteDH = drilingInDto.AOdH,
                AbsoluteHEffective = drilingInDto.AOHef,
                DXB = drilingInDto.dXT,
                DYB = drilingInDto.dYT,
                DXH = drilingInDto.dXB,
                DYH = drilingInDto.dYB
            };

           

            return drillingIn;
        }

        private WellClass.Well.VskrutCl ConvertItemBack(DrillingIn drilingIn)
        {
            var drillingInDto = new WellClass.Well.VskrutCl
            {
                ID = drilingIn.Id,
                Plast = drilingIn.Reservoir,
                Source = drilingIn.Source,
                Top = drilingIn.Top,
                Baze = drilingIn.Base,
                Hef = drilingIn.HEffective,
                Note = drilingIn.Remark,
                AOTop = drilingIn.AbsoluteTop,
                AOBaze = drilingIn.AbsoluteBase,
                dXT = drilingIn.DXB,
                dYT = drilingIn.DYB,
                dXB = drilingIn.DXH,
                dYB = drilingIn.DYH
            };
            
            return drillingInDto;
        }
    }
}
