using System;
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures
{
    public class ReservoirPressureInformation
    {
        public string Uwi { get; set; } = string.Empty;
        public ICollection<PressurePoint> SurveysData { get; set; } = new List<PressurePoint>();
        public ICollection<PressurePoint> ReportData { get; set; } = new List<PressurePoint>();
        public ICollection<PressurePoint> ModelData { get; set; } = new List<PressurePoint>();
        public PressureCalculationMethod PressureCalculationMethod { get; set; } = PressureCalculationMethod.None;
        public double CoefficientA { get; set; }
        public double CoefficientB { get; set; }
        public DateTime ABDate { get; set; }
        public double ModelPressureCoefficient { get; set; }
        public double ModelAngleCoefficient { get; set; }
        public DateTime ModelAngleDatePoint { get; set; }
        public double ModelScaleLine { get; set; }
        public double ModelScale { get; set; }
    }
}
