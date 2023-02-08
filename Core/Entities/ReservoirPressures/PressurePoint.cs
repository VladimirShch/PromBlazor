using System;

namespace Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures
{
    public class PressurePoint
    {
        public PressurePoint(double value, double date, bool enable)
        {
            Value = value;
            OADate = date;
            Enable = enable;
        }

        public PressurePoint(double value, DateTime date, bool enable)
        {
            Value = value;
            Date = date;
            Enable = enable;
        }

        public int Id { get; set; }
        public double Value { get; set; }
        public int SurveyType { get; set; }
        public PressureSourceType Source { get; set; } = PressureSourceType.Undefined;
        public DateTime Date { get; set; }
        public double OADate
        {
            get => Date.ToOADate();
            set
            {
                Date = DateTime.FromOADate(value);
            }
        }
        public bool Enable { get; set; }
    }
}
