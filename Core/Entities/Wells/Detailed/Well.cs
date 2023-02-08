using System;
using System.Collections.Generic;
using Web_Prom.Core.Blazor.Core.Entities.Bottomholes;
using Web_Prom.Core.Blazor.Core.Entities.WellContacts;
using Web_Prom.Core.Blazor.Core.Entities.HydrodynamicCharacteristics;
using Web_Prom.Core.Blazor.Core.Entities.OpenedIntervals;
using Web_Prom.Core.Blazor.Core.Entities.Perforations;
using Web_Prom.Core.Blazor.Core.Entities.Stratigraphy;
using Web_Prom.Core.Blazor.Core.Entities.Trajectories;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.Core.Entities.WellJobs;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Enums;
using Web_Prom.Core.Blazor.Core.Entities.WellTypesHistory;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed
{
    public class Well
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; } // int?

        public int Area { get; set; } // ! Тип! Площадь

        public int Region { get; set; } //! Тип! Район

        public string PlantId { get; set; } // УКПГ

        public int WellGroup { get; set; } // ! Тип! Куст

        public float Altitude { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public WellShape Shape { get; set; }

        public DateTime BalanceStartDate { get; set; }

        public DateTime StartWorkDate { get; set; }

        public float Lambda { get; set; }

        public float Water { get; set; }

        public float Roughness { get; set; }

        public float BottomholeMd { get; set; }
        public float BottomholeTvd { get; set; }
        public float BottomholeDx { get; set; }
        public float BottomholeDy { get; set; } // Почему я не сделал изначально или убрал позже? Не используется в справочнике скважин? В старом Промысле вроде нет
        public int[]? DevelopmentObjectCalculated { get; set; }
        public int DevelopmentObjectMain { get; set; }
        public int DevelopmentObjectAdditional { get; set; }

        // Project--
        public WellType TypeByProject { get; set; }
        public float BottomholeByProject { get; set; }
        public int HorizonByProject { get; set; }
        public int Project { get; set; }

        // В отдельном Dto-классе и, вероятно, в отдельной таблице ---------------------
        public WellType Type { get; set; }

        public WellState State { get; set; }

        public float PerforationIntervalMiddleMd { get; set; }
        public float PerforationIntervalMiddleTvd { get; set; }

        public float ReferencePlaneMd { get; set; } // плоскость приведения
        public float ReferencePlandeTvd { get; set; }

        public float CurrentBottomholeMd { get; set; } // текущий забой
        public float CurrentBottomholeTvd { get; set; }
        public DateTime CurrentBottomholeDate { get; set; }

        public float ArtificialBottomholeMd { get; set; }
        public float ArtificialBottomholeTvd { get; set; }
        public ICollection<WellTypeChange> WellTypeChanges { get; set; } = new List<WellTypeChange>();
        //-----------------------------------------------------------------------
        public ICollection<WellJob> WellJobs { get; set; } = new List<WellJob>();
        //-----------------------------------------------------------------------
        public ICollection<WellConstructionItem> Construction { get; set; } = new List<WellConstructionItem>();
        public ICollection<Perforation> Perforations { get; set; } = new List<Perforation>();
        public ICollection<OpenedInterval> OpenedIntervals { get; set; } = new List<OpenedInterval>();
        // TODO: почему я решил траектории получать отдельно на чтение?
        public ICollection<TrajectoryStation> Trajectory { get; set; } = new List<TrajectoryStation>();
        public ICollection<DrillingIn> Stratigraphy { get; set; } = new List<DrillingIn>();
        public ICollection<Bottomhole> BottomholeHistory { get; set; } = new List<Bottomhole>();
        public ICollection<HydrodynamicCharacteristic> HydrodynamicCharacteristicHistory { get; set; } = new List<HydrodynamicCharacteristic>();
        public ICollection<WellContact> Contacts { get; set; } = new List<WellContact>();
    }
}
