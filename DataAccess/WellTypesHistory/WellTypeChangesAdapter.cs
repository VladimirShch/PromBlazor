using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.WellTypesHistory;
using Web_Prom.Core.Blazor.DataAccess.Common;
using Web_Prom.Core.Blazor.DataAccess.Wells.Common;

namespace Web_Prom.Core.Blazor.DataAccess.WellTypesHistory
{
    public class WellTypeChangesAdapter : IAdapter<WellClass.HistoryClass[]?, ICollection<WellTypeChange>>
    {
        private readonly WellPropertiesConverter _propertiesConverter;

        public WellTypeChangesAdapter(WellPropertiesConverter propertiesConverter)
        {
            _propertiesConverter = propertiesConverter;
        }

        public ICollection<WellTypeChange> Convert(WellClass.HistoryClass[]? historyDto)
        {
            if(historyDto is null)
            {
                return new List<WellTypeChange>();
            }
            ICollection<WellTypeChange> wellTypeChanges = historyDto.Where(t => t is not null).Select(ConvertItem).ToList();

            return wellTypeChanges;
        }

        public WellClass.HistoryClass[]? ConvertBack(ICollection<WellTypeChange> wellTypeChanges)
        {
            if(wellTypeChanges is null)
            {
                return null;
            }
            WellClass.HistoryClass[] historyDto = wellTypeChanges.Where(t => t is not null).Select(ConvertItemBack).ToArray();
            
            return historyDto;
        }

        private WellTypeChange ConvertItem(WellClass.HistoryClass typeChangeDto)
        {
            if(typeChangeDto is null)
            {
                throw new ArgumentNullException(nameof(typeChangeDto));
            }

            var wellTypeChange = new WellTypeChange
            {
                Id = typeChangeDto.ID,
                Date = typeChangeDto.Dates,
                WellType = _propertiesConverter.ConvertType(typeChangeDto.Code)
            };

            return wellTypeChange;
        }

        private WellClass.HistoryClass ConvertItemBack(WellTypeChange typeChange)
        {
            if (typeChange is null)
            {
                throw new ArgumentNullException(nameof(typeChange));
            }

            var wellTypeChange = new WellClass.HistoryClass
            {
                ID = typeChange.Id,
                Dates = typeChange.Date,
                Code = _propertiesConverter.ConvertTypeBack(typeChange.WellType)
            };

            return wellTypeChange;
        }
    }
}
