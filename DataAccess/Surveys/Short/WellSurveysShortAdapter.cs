using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Short;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Short
{
    public class WellSurveysShortAdapter : IAdapter<IEnumerable<WellClass.Well.IsslCs>?, ICollection<WellSurveyShort>>
    {
        public ICollection<WellSurveyShort> Convert(IEnumerable<WellClass.Well.IsslCs>? wellSurveysDto)
        {
            if(wellSurveysDto is null)
            {
                return new List<WellSurveyShort>();
            }

            ICollection<WellSurveyShort> wellSurveysShort = wellSurveysDto.Where(t => t is not null).Select(Convert).ToList();

            return wellSurveysShort;
        }
     
        private WellSurveyShort Convert(WellClass.Well.IsslCs wellSurveyDto)
        {
            if(wellSurveyDto is null)
            {
                throw new ArgumentNullException(nameof(wellSurveyDto));
            }

            var wellSurveyShort = new WellSurveyShort
            {
                Id = wellSurveyDto.ID.ToString(),
                Name = wellSurveyDto.TypeStr.PadRight(5) + wellSurveyDto.Finish.ToString("dd.MM.yy")
            };

            return wellSurveyShort;
        }

        public IEnumerable<WellClass.Well.IsslCs>? ConvertBack(ICollection<WellSurveyShort> itemFrom)
        {
            throw new NotImplementedException();
        }

    }
}
