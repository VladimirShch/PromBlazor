
namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class WellSurveyJob
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Organization { get; set; }
        public int Contractor { get; set; }
        public int ResponsiblePerson { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
