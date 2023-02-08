using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Perforations;

namespace Web_Prom.Core.Blazor.DataAccess.WellPerforations
{
    public class PerforationAdapter : IPerforationAdapter
    {
        public ICollection<Perforation> Convert(WellClass.Well.PerfCs[]? perforationsDto, IEnumerable<WellClass.Well.VskrutCl> openingsDto)
        {
            if (perforationsDto is null)
            {
                return new List<Perforation>();
            }
            ICollection<Perforation> resultPerforations = perforationsDto.Where(t => t is not null).Select(t => ConvertItem(t, openingsDto)).ToList();

            return resultPerforations;
        }

        public WellClass.Well.PerfCs[]? ConvertBack(ICollection<Perforation> perforations)
        {
            if (perforations is null)
            {
                return Array.Empty<WellClass.Well.PerfCs>();
            }

            WellClass.Well.PerfCs[] perforationsDto = perforations.Where(t => t is not null).Select(ConvertItemBack).ToArray();

            return perforationsDto;
        }

        private Perforation ConvertItem(WellClass.Well.PerfCs perforationDto, IEnumerable<WellClass.Well.VskrutCl> openingsDto)
        {
            if(perforationDto is null)
            {
                throw new ArgumentNullException(nameof(perforationDto));
            }
            
            var resultPerforation = new Perforation
            {
                Id = perforationDto.ID, 
                Top = perforationDto.Top,
                Base = perforationDto.Baze,
                OpenDate = perforationDto.OpenDate,
                CloseDate = perforationDto.CloseDate,
                PerforationType = perforationDto.Type, //!!!!Сделать преобразование типов
                DH = perforationDto.dH,
                HEffective = perforationDto.Hef,
                KProd = perforationDto.K_prod,
                WasHydrofrac = perforationDto.GRP,
                HydrofracDate = perforationDto.GRPDate,
                AbsoluteTop = perforationDto.AOTop,
                AbsoluteBase = perforationDto.AOBaze,
                AbsoluteDH = perforationDto.AOdH,
                AbsoluteHEffective = perforationDto.AOHef,
                // TODO: непосредственно используется сервис, не инжектится
                Reservoirs = ReservoirPerforationService.ReservoirsByInterval(openingsDto, perforationDto.Top, perforationDto.Baze),
                Remark = perforationDto.Note
            };

            return resultPerforation;
        }

        private WellClass.Well.PerfCs ConvertItemBack(Perforation perforationItem)
        {
            if (perforationItem is null)
            {
                throw new ArgumentNullException(nameof(perforationItem));
            }

            var perforationDto = new WellClass.Well.PerfCs
            {
                ID = perforationItem.Id,
                Top = perforationItem.Top,
                Baze = perforationItem.Base,
                OpenDate = perforationItem.OpenDate,
                CloseDate = perforationItem.CloseDate,
                Type = perforationItem.PerforationType, //!!!!Сделать преобразование типов
                dH = perforationItem.DH,
                Hef = perforationItem.HEffective,
                K_prod = perforationItem.KProd,
                GRP = perforationItem.WasHydrofrac,
                GRPDate = perforationItem.HydrofracDate,
                AOTop = perforationItem.AbsoluteTop,
                AOBaze = perforationItem.AbsoluteBase,
                AOdH = perforationItem.AbsoluteDH,
                AOHef = perforationItem.AbsoluteHEffective,
                Note = perforationItem.Remark
            };

            return perforationDto;
        }
    }
}
