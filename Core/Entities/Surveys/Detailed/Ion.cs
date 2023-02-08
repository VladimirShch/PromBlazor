namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class Ion
    {
        public Ion(float molarMass)
        {
            MolarMass = molarMass;
        }
        public float MilligramLiter { get; set; } = -999; // содержание мг/л
        public float MolarMass { get; }   // мольная масса
    }
}
