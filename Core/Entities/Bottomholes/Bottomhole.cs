using System;

namespace Web_Prom.Core.Blazor.Core.Entities.Bottomholes
{
    public class Bottomhole
    {
        public int Id { get; set; }
        public float Depth { get; set; } 
        public DateTime Date { get; set; }
        public int Survey { get; set; } 
    }
}
