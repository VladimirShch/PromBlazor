
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    // TODO: глубинка - придумать имя получше
    public class DeepSurvey
    {       
        public float TemperatureAtm { get; set; } // температура воздуха для расчета растяжения проволоки
        public float LoadDiameter { get; set; }    // длина диаметр груза, мм // TODO: перевести корректно
        public float LoadLength { get; set; }
        public string Remark { get; set; }
        public ICollection<DeepSurveyStation> DeepSurveyStations { get; set; } = new List<DeepSurveyStation>();        
    }
}
