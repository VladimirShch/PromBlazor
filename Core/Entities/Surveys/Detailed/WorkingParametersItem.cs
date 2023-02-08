using System;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class WorkingParametersItem
    {
        // Вероятно, нарушение, так как в предметной области никакого Id не надо, это по сути, как написано в Dto, номер строки в базе данных
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float WellheadPressure { get; set; } //BufferPressure ?
        public float ManifoldPressure { get; set; }
        public float AnnularPressure { get; set; }
        public float WellheadTemperature { get; set; }
        public float Flowrate { get; set; } //дебит, посмотреть подходящий перевод
        public float CasingPressureBase { get; set; }
        public float CasingPressureTop { get; set; }
        public float LiquidLevel { get; set; }
        public float WaterLevel { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
