using System;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    // Переименовать свойства, все взяты напрямую из старого WellClass, который используется в качестве dto
    public class HydrochemicalAnalysis
    {
        public int Id { get; set; } // ссылка на идентификатор в базе
        public DateTime DateAn { get; set; }
        public Ion K { get; set; } = new(39.102f);
        public Ion Na { get; set; } = new(22.991f);
        public Ion Ca { get; set; } = new(20.04f);
        public Ion Mg { get; set; } = new(12.156f);
        public Ion Sr { get; set; } = new(43.81f);
        public Ion NH4 { get; set; } = new(18.04f);
        public Ion Fe { get; set; } = new(18.616f);
        public Ion Cl { get; set; } = new(35.453f);
        public Ion SO4 { get; set; } = new(48.031f);
        public Ion HCO3 { get; set; } = new(1.017f);
        public Ion CO3 { get; set; } = new(30.005f);
        public Ion H2PO4 { get; set; } = new(93.91696f);
        public Ion B { get; set; } = new(3.604f);
        public Ion Br { get; set; } = new(79.9f);
        public Ion I { get; set; } = new(126.9f);
        public float PH { get; set; } = -999;
        public float DryResidue { get; set; } = -999; // сухой остаток
        public float APAP { get; set; } = -999;
        public float Vzvesh { get; set; } = -999; // взвешенные вещества
        public float O2 { get; set; } = -999; // свободный кислород
        public float UV_ob { get; set; } = -999;   // объемная доля углеводородной части
        public float UV_po { get; set; } = -999;    // плотнось углеводородной части
        public float VMV_po { get; set; } // плотность водометанольной смеси
        public string VMV_СH3OH { get; set; } = ""; // содержание метанола в водометанольной части
        public string Remark { get; set; } = "";
        public string FluidTypeGeochemical { get; set; } = "";
        public bool WT_Question { get; set; }
        public bool WT_K { get; set; }
        public bool WT_T { get; set; }
        public bool WT_PL { get; set; }
        public bool WT_PL_unknown { get; set; }
        public bool WT_PL_sen { get; set; }
        public bool WT_PL_neo { get; set; }
        public bool WT_PL_Ach { get; set; }
        public bool WT_C5 { get; set; }
        public bool WT_CH5OH { get; set; }
    }
}
