using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.DataAccess.Common;
using static WellClass.Well.IsslCs;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.SurveyJobs
{
    public class WellSurveyJobsAdapter : IAdapter<isslWorksCL[], ICollection<WellSurveyJob>>
    {
        public ICollection<WellSurveyJob> Convert(isslWorksCL[]? surveyJobsDto)
        {
            if (surveyJobsDto is null)
            {
                return new List<WellSurveyJob>();
            }

            ICollection<WellSurveyJob> resutlSurveyJobs = surveyJobsDto.Where(t => t is not null).Select(Convert).ToList();

            return resutlSurveyJobs;
        }

        public isslWorksCL[] ConvertBack(ICollection<WellSurveyJob> surveyJobs)
        {
            // TODO: посмотреть, что нужно возвращать!
            if(surveyJobs is null)
            {
                return null;
            }
            isslWorksCL[] surveyJobsDto = surveyJobs.Where(t => t is not null).Select(ConvertBack).ToArray();

            return surveyJobsDto;
        }

        private WellSurveyJob Convert(isslWorksCL surveyJobDto)
        {
            if (surveyJobDto is null)
            {
                throw new ArgumentNullException(nameof(surveyJobDto));
            }

            var wellSurveyJob = new WellSurveyJob
            {
                Id = surveyJobDto.ID,
                Type = surveyJobDto.Work,
                Organization = surveyJobDto.Org,
                ResponsiblePerson = surveyJobDto.Woker,
                Remark = surveyJobDto.Note
            };

            return wellSurveyJob;
        }

        private isslWorksCL ConvertBack(WellSurveyJob surveyJob)
        {
            if (surveyJob is null)
            {
                throw new ArgumentNullException(nameof(surveyJob));
            }

            var surveyJobDto = new isslWorksCL
            {
                ID = surveyJob.Id == -999 ? default : surveyJob.Id, // TODO: избавиться!
                Work = surveyJob.Type,
                Org = surveyJob.Organization,
                Woker = surveyJob.ResponsiblePerson,
                Note = surveyJob.Remark
            };

            return surveyJobDto;
        }
    }
}
