namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class StaticSurvey
    {
        public int ReservoirPressureCalculationMethod { get; set; } // способ расчета пластового давления(забойное статическое)
        public float ReservoirPressurePerforationMiddle { get; set; } // пластовое давление на середину интервала перфорации
        public float ReservoirPressureReferencePlane { get; set; } // пластовое давление на плоскость приведения
        public float WellheadPressure { get; set; } // устьевое статическое давление
        public float AnnulusPresure { get; set; } // затрубное статическое давление //OutsideTubingPressure? CasingPressure?
        public float DepthPressure { get; set; } // статическое давление по данным глубинного замера //Subsurface?
        public float Depth { get; set; } // глубина глубинного замера
        public bool IsOff { get; set; }// выклычение замера из прогноза пластового давления
        public float CasingPressureBase { get; set; } // нижняя межколонка //intercasing?
        public float CasingPressureTop { get; set; } // верхняя межколонка
        public float LiquidLevel { get; set; } // Уровень жидкости в стволе
        public float WaterLevel { get; set; }// уровень воды в стволе
        public string? Remark1 { get; set; } // примечание к статическому замеру
        public string? Remark2 { get; set; } // генерируемое примечание о расчете
                                            // Public StopTime As DateInterval 'время простоя скважины перед замером
    }
}
