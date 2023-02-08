using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.WellJobs;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.WellJobs
{
    public class WellJobsAdapter : IAdapter<WellClass.Well.WorksCl?, ICollection<WellJob>>
    {
        public ICollection<WellJob> Convert(WellClass.Well.WorksCl? wellWorksDto)
        {
            if(wellWorksDto is null || wellWorksDto.Work is null)
            {
                return new List<WellJob>();
            }
            ICollection<WellJob> wellJobs = wellWorksDto.Work.Where(t => t is not null).Select(ConvertItem).ToList();
            return wellJobs;
        }

        public WellClass.Well.WorksCl? ConvertBack(ICollection<WellJob> wellJobs)
        {
            if (wellJobs is null)
            {
                return null;
            }
            var workDto = new WellClass.Well.WorksCl
            {
                Work = wellJobs.Where(t => t is not null).Select(ConvertItemBack).ToArray()
            };
            return workDto;
        }

        private WellJob ConvertItem(WellClass.Well.WorksCl.WorkCL workDto)
        {
            if(workDto is null)
            {
                throw new ArgumentNullException(nameof(workDto));
            }

            var wellJob = new WellJob
            {
                Id = workDto.ID,
                DecisionDate = workDto.DateItin,
                Start = workDto.Date1,
                End = workDto.Date2,
                JobType = workDto.WorkType,
                Contractor = workDto.WOker,
                Remark = workDto.Note
            };

            return wellJob;
        }

        private WellClass.Well.WorksCl.WorkCL ConvertItemBack(WellJob wellJob)
        {
            if (wellJob is null)
            {
                throw new ArgumentNullException(nameof(wellJob));
            }

            var dto = new WellClass.Well.WorksCl.WorkCL
            {
                ID = wellJob.Id,
                DateItin = wellJob.DecisionDate,
                Date1 = wellJob.Start,
                Date2 = wellJob.End,
                WorkType = wellJob.JobType,
                WOker = wellJob.Contractor,
                Note = wellJob.Remark
            };

            return dto;
        }
    }
}
