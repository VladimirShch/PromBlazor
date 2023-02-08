using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class PressureBuildUpCurve
    {
        public float A { get; set; }
        public float B { get; set; }
        public float Depth { get; set; }
        public ICollection<BuildUpCurvePoint> Points { get; set; } = new List<BuildUpCurvePoint>();
        public string Remark { get; set; } = string.Empty;
    }

    public class BuildUpCurvePoint
    {
        public int Id { get; set; } // номер строки в базе данных
        // Public Np As Integer 'номер точки замера
        public double Time { get; set; }   // СЕКУНДЫ
        public float Pressure { get; set; } // давление
    }
}
