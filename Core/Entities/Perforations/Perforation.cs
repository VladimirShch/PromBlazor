using System;
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Perforations
{
    public class Perforation
    {
        public int Id { get; set; }
        public float Top { get; set; }
        public float Base { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int PerforationType { get; set; } //!!!!! Сделать трансляцию!
        public string ReservoirName { get; set; } = string.Empty; // Во-первых, посмотреть, как корректно перевести "пласт", во вторых, если это сохраняемое поле, то string не пойдёт
        public IEnumerable<int> Reservoirs { get; set; } = new List<int> { -999 };
        public float DH { get; set; }
        public float HEffective { get; set; }
        public float KProd { get; set; }
        public bool WasHydrofrac { get; set; } // ГРП
        public DateTime HydrofracDate { get; set; }
        public float AbsoluteTop { get; set; }
        public float AbsoluteBase { get; set; }
        public float AbsoluteDH { get; set; }
        public float AbsoluteHEffective { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
