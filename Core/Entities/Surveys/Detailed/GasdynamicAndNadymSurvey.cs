
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class GasdynamicAndNadymSurvey
    {
        public int Id { get; set; }
        public int TypeQ { get; set; } // спобоб определения дебита
        public int BottomholePressureCalculationMethod { get; set; } // способ расчета забойного давления
        public bool NoAdiab { get; set; } // без учета адиабаты
        public bool IsBad { get; set; } // отключает использование коэф.
        public int DictDiameter { get; set; } // диаметр дикта // diaphragm gauge of critical flow? диафрагменный измеритель критического течения
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public float Theta { get; set; }
        public float E2s { get; set; }
        public float Exponent { get; set; } // показатель степени
        public string Remark { get; set; } // примечание с ГДИ
        public float Qc { get; set; } // свободный дебит
        public float Qac { get; set; } // абсолютно свободный дебит
        public float APipesim { get; set; }
        public float BPipesim { get; set; }
        public string PSAmes { get; set; }
        public string PSBmes { get; set; }
        public ICollection<GasDynamicRegime> GasDynamicRegimes { get; set; } = new List<GasDynamicRegime>();
    }
}
