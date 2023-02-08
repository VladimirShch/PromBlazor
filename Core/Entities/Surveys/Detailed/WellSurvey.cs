using System;
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class WellSurvey
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } // TODO: сделать типы хардкод enum внутри программы или читать из базы дополнительную сущность Type или Code
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public string Recommendations { get; set; }
        public string Remark { get; set; }
        public string Automobile { get; set; }
        public ICollection<WellSurveyJob> Jobs { get; set; } = new List<WellSurveyJob>();
        public Templating? Templating { get; set; }
        public ICollection<WorkingParametersItem> WorkingParameters { get; set; } = new List<WorkingParametersItem>();
        public GasCondensateSurvey? GasCondensateSurvey { get; set; }
        public StaticSurvey? StaticSurvey { get; set; }
        public DeepSurvey? DeepSurvey { get; set; }
        public GasdynamicAndNadymSurvey? GasdynamicAndNadymSurvey { get; set; }
        public GeophysicalSurvey? GeophysicalSurvey { get; set; }
        public PressureBuildUpCurve? Pbu { get; set; }
        public ICollection<HydrochemicalAnalysis> HydrochemicalAnalysis { get; set; } = new List<HydrochemicalAnalysis>();
    }
}
