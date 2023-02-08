namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class DeepSurveyStation
    {
        public int Id { get; set; } // ссылка на запись в базе
        public float HeightMeasured { get; set; } // замер по лебёдке
        public float HeightWellbore { get; set; } // глубина по стволу
        public float HeightVertical { get; set; }
        public float HeightAbsolute { get; set; }
        public float Pressure { get; set; } // давление
        public float Temperature { get; set; } // температура
        public float Density { get; set; }
        public string Liquid { get; set; }
        public string Remark { get; set; }
    }
}
