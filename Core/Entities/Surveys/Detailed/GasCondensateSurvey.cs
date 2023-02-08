namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class GasCondensateSurvey
    {
        public float ReservoirGasDensity { get; set; }
        public float CriticalPressure { get; set; }
        public float CriticalTemperature { get; set; }
        public float SeparationGasMoleFraction { get; set; } // мольная доля газа сепарации в пластовом газе
        public float DryGasMoleFraction { get; set; } // мольная доля сухого газа в пластовом газе
        public float PC5Reservoir { get; set; } // потенц. сод конд в пластовом газе // PotentialContent?
        public float PC5Separation { get; set; } // потенц. сод. конд в газе сепарации
        public float PC5Dry{ get; set; } // потенц. сод. конд в сухом газе
        public float MReservoir { get; set; } // мольная доля газа сепарации в пластовом газе ??? см SeparationGasMoleFraction
        public float MC5 { get; set; }
        public float ShrinkCoefficient { get; set; } // TODO: Коэффициент усадки - подобрать перевод корректный //contraction coefficient; shrinkage factor //formation oil volumetric factor
        public float N2 { get; set; }
        public float CO2 { get; set; }
        public string Remark { get; set; }
    }
}
