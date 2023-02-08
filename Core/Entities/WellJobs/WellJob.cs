using System;

namespace Web_Prom.Core.Blazor.Core.Entities.WellJobs
{
    public class WellJob
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DecisionDate { get; set; }
        public int JobType { get; set; }
        public int Contractor { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
