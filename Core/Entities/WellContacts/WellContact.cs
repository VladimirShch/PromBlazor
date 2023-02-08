using System;

namespace Web_Prom.Core.Blazor.Core.Entities.WellContacts
{
    public class WellContact
    {
        public int Id { get; set; }
        public bool WasSurvey { get; set; }
        public int ContactType { get; set; }
        public DateTime Date { get; set; }
        public float Top { get; set; }
        public float Base { get; set; }
        public float TopAbsolute { get; set; }
        public float BaseAbsolute { get; set; }
        public float Temperature { get; set; }
        public int Reservoir { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}
