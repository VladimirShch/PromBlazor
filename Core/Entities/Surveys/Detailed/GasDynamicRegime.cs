using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public enum FlowrateDeterminationMethod
    {
        [Description("Неизвестно")]
        Unknown = 0,
        [Description("Замер")]
        Measuring = 1,
        [Description("СТО")]
        STO = 2,
        [Description("База")]
        Base = 3
    }

    // Комментарии Сергея из старого промысла
    public class GasDynamicRegime
    {
        public int IdGasdynamicSurvey { get; set; } // определитель исседованиясв
        public int IdRegime { get; set; } // определитель исследования для с связи с надымом
        public bool UseSeparator { get; set; } // наличие сепаратора
        public int RegimeIndex { get; set; }// порядковый номер режима
        public DateTime OnDate { get; set; }// временная привязка режима
        public float RegimeDuration { get; set; } // время на режиме в минутах
        public float DiaphragmDiameter { get; set; } // диаметр диафрагмы
        public float BufferPressure { get; set; } // буферное давление
        public float ManifoldPressure { get; set; } // давление на манифольде
        public float DictPressure { get; set; } // давление ДИКТа
        public float AnnulusPressure { get; set; } // затрубное давление на режиме
        public float BufferTemperature { get; set; } // температура на буфере(Манифольд)
        public float DictTemperature { get; set; } // температура на дикте
        public float ReservoirPressure { get; set; } // забойное давление
        public float DepthPressure { get; set; } // давление по данным глубинного замере
        public float DepthBase { get; set; } // глубина замера
        public float ReservoirDepression { get; set; } // депрессия на пласт
        public float ReservoirDepressionSquare { get; set; } // квадратичная депрессия
        public FlowrateDeterminationMethod FlowrateDeterminationMethod { get; set; } // способ определения дебита
        public float Flowrate { get; set; } // дебит
        public bool IsOff { get; set; }  // брак (исключение режима из расчета)
        public float CasingPressureBase { get; set; } // нижняя межколонка
        public float CasingPressureTop { get; set; } // верхняя межколонка
        public float LiquidLevel { get; set; } // Уровень жидкости в стволе
        public float WaterLevel { get; set; } // уровень воды в стволе
        public int ReservoirPressureDeterminationMethod { get; set; } // способ расчета забойного давления
        public string TubingMode { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;// примечание к режиму

        // Public Nadum As NadumClass
        public float UniformityCoefficient { get; set; } // коэф.однородности. Надеюсь он здесь временно, пока не 
                                                         // разберусь с расчетом исследования

        // временное поле для сопоставления режима в MDB
        public int TempId { get; set; }
        public float Lambda { get; set; } // лямбда на режиме

        // Надым (химия) относится к режиму
        public float Qwater { get; set; }
        public float Qsand { get; set; }
        public string RemarkNadym { get; set; } = string.Empty;
        public ICollection<GSU> GSU { get; set; } = new List<GSU>();
        public float Tank { get; set; }
        public float Wwater
        {
            get
            {
                if (Flowrate > 0f & Qwater >= 0f & RegimeDuration > 0f)
                {
                    return Qwater / (Flowrate * RegimeDuration / 24f / 60f);
                }
                else
                {
                    return -999;
                }
            }
        }
        public float WSand
        {
            get
            {
                if (Flowrate > 0f & Qsand >= 0f & RegimeDuration > 0f)
                {
                    return Qsand / (Flowrate * RegimeDuration / 24f / 60f);
                }
                else
                {
                    return -999;
                }
            }
        }
    }

    // Что-нибудь с этим классом сделать - перенести в отдельный файл, переименовать, etc.
    public class GSU
    {
        // время отбора в секундах
        public int Id { get; set; }
        public int Secs { get; set; }
        public float Vwat { get; set; } // отбор жидкости M3
        public float Vmp { get; set; } // отбор мехпримесей гр
        public float C5Saving { get; set; }
        public string Remark { get; set; } = string.Empty;

        public float Qwat
        {
            get
            {
                float rez = -999;
                if (Secs > 0 & Vwat > 0f)
                {
                    rez = Vwat / Secs * 60f * 60f * 24f;
                }

                return rez;
            }
        }

        public float Qmp
        {
            get
            {
                float rez = -999;
                if (Secs > 0 & Vmp > 0f)
                {
                    rez = Vmp / Secs * 60f * 60f * 24f * 1000f;
                }

                return rez;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                int seconds = Secs;
                int hours = (int)Math.Floor(seconds / 3600d);
                seconds -= hours * 3600;
                int mins = (int)Math.Floor(seconds / 60d);
                seconds -= mins * 60;
                int resultSeconds = seconds;
                return new TimeSpan(hours, mins, resultSeconds);
            }

            set
            {
                Secs = value.Hours * 3600 + value.Minutes * 60 + value.Seconds;
            }
        }
    }
}
