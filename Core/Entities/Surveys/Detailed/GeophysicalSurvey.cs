using System;
using System.Collections.Generic;

namespace Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed
{
    public class GeophysicalSurvey
    {
        public ICollection<ACC> ACC { get; set; } = new List<ACC>(); // массив интервалов АКЦ
        public ICollection<InflowInterval> InflowIntervals { get; set; } = new List<InflowInterval>(); // массив интервалов притока
                                                                                                       // Public Contacts As GISContaktsClass 'контакты
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>(); // контакты
        public ICollection<ConstructionItem> Construction { get; set; } = new List<ConstructionItem>(); // Уточнение конструкции
        public ICollection<GeophysicalSurveyPerforation> PerforationIntervals { get; set; } = new List<GeophysicalSurveyPerforation>(); // уточнение интервалов перфорации
                                                                                                                                        // Public NKT As Single 'отбивка НКТ
        public float Stopping { get; set; }// остановка прибора
        public float CurrentBottomhole { get; set; } // текущий забой
        public float CementBridge { get; set; } // цементный мост
    }

    // TODO: вынести отдельно! ----------------------------------------
    public class ConstructionItem
    {
        public int Id { get; set; }
        public int ConstructionElement { get; set; } // элемент конструкции
        public float Top1 { get; set; }   // ЗАЯВЛЕНО
        public float Base1 { get; set; }
        public float Top2 { get; set; }   // уточнение
        public float Base2 { get; set; }
        public float Diameter { get; set; }
        public int InstallationPlace { get; set; } // место установки оборудования(оборудование)
        public string Remark { get; set; }
        public float AbsoluteTop2 { get; set; } // АО
        public float AbsoluteBase2 { get; set; }
        public float AbsoluteTop1 { get; set; }
        public float AbsoluteBase1 { get; set; }
    }

    public class GeophysicalSurveyPerforation
    {
        public int Id { get; set; }
        public int Reservoir { get; set; }
        public float Top1 { get; set; }   // Заявлено
        public float Base1 { get; set; }
        public float Top2 { get; set; }   // замерено
        public float Base2 { get; set; }
        public string Remark { get; set; } // примечание
        public float AbsoluteTop2 { get; set; }
        public float AbsoluteBase2 { get; set; }
        public float AbsoluteTop1 { get; set; }
        public float AbsoluteBase1 { get; set; }
    }

    public class InflowInterval
    {
        public int Id { get; set; }    // код интервала притока
        public int Reservoir { get; set; }
        public int Regime { get; set; } // код режима
        public float Top { get; set; }
        public float Base { get; set; }
        public int InflowType { get; set; } // тип притока // TODO: перевести корректно!
        public float HeightEffective { get; set; }
        public string ReservoirProperties { get; set; } // FES фильтрационно емкостные свойства
        public float Flowrate { get; set; } // дебит на режиме в интервале
        public bool Hydrofrac { get; set; } // грп в интервале
        public string Remark { get; set; }
        public float AbsoluteTop { get; set; }
        public float AbsoluteBase { get; set; }
        // TODO: это для отображения. Убрать?? И имя неправильное >>>>>>>>>>>>>>>>>>
        public float? HeightWork => Top > 0f && Base > 0f ? (float)Math.Round(Base - Top, 2) : null;
        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    }

    public class ACC
    {
        public float Id { get; set; }
        public float Top { get; set; }
        public float Base { get; set; }
        public float DH { get; set; }
        public int CMCol { get; set; }
        public int CMFormation { get; set; } // порода
        public int Backside { get; set; } // Zatrub // TODO: перевести нормально!
        public string Remark { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; } // ССЫЛКА НА ЗАПИСЬ В БАЗЕ
        public int SurveyId { get; set; }// ссылка на исследование
        public int Type { get; set; } // вид контакта
        public float Top { get; set; }
        public float Base { get; set; }
        public DateTime Date { get; set; }
        public int Reservoir { get; set; } // ссылка на пласт
        public float Temperature { get; set; }  // температура
        public float AbsoluteTop { get; set; }
        public float AbsoluteBase { get; set; }
        public string Remark { get; set; }
    }
}
