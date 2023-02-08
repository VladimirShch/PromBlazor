using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.OpenedIntervals
{
    public class OpenedInterval
    {
        public IEnumerable<int> Reservoirs { get; set; } = new List<int> { -999 };
        public float Top { get; set; }
        public float Base { get; set; }
        public float DH { get; set; }       
    }
}
