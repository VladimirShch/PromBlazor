
namespace Web_Prom.Core.Blazor.Core.Entities.Stratigraphy
{
    public class DrillingIn
    {
        public int Id { get; set; }
        public int Source { get; set; }
        public int Reservoir { get; set; }
        public float Top { get; set; }
        public float Base { get; set; }
        public float DH { get; set; }
        public float HEffective { get; set; }
        public float Prod { get; set; }
        public string Remark { get; set; }
        //----------------------------------
        public float AbsoluteTop { get; set; }
        public float AbsoluteBase { get; set; }
        public float AbsoluteDH { get; set; }
        public float AbsoluteHEffective { get; set; }
        public float DXB { get; set; }
        public float DYB { get; set; }
        public float DXH { get; set; }
        public float DYH { get; set; }
    }
}
