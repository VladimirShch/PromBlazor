using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.HydrodynamicCharacteristics
{
    public class HydrodynamicCharacteristic
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsBadSurvey { get; set; }
        public float A { get; set; }
        public float B { get; set; }
        public float E2s { get; set; }
        public float Theta { get; set; }
    }
}
