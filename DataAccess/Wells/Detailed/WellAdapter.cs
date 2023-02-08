using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Bottomholes;
using Web_Prom.Core.Blazor.Core.Entities.WellContacts;
using Web_Prom.Core.Blazor.Core.Entities.HydrodynamicCharacteristics;
using Web_Prom.Core.Blazor.Core.Entities.OpenedIntervals;
using Web_Prom.Core.Blazor.Core.Entities.Stratigraphy;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.Core.Entities.WellJobs;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.WellTypesHistory;
using Web_Prom.Core.Blazor.DataAccess.Common;
using Web_Prom.Core.Blazor.DataAccess.OpenedIntervals;
using Web_Prom.Core.Blazor.DataAccess.WellConstructions;
using Web_Prom.Core.Blazor.DataAccess.WellPerforations;
using Web_Prom.Core.Blazor.DataAccess.Wells.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Detailed
{
    public class WellAdapter : IWellAdapter
    {
        private readonly WellPropertiesConverter _propertiesConverter;
        private readonly IAdapter<WellClass.Well.WorksCl?, ICollection<WellJob>> _wellJobsAdapter;
        private readonly IAdapter<WellClass.HistoryClass[]?, ICollection<WellTypeChange>> _wellTypeChangesAdapter;
        private readonly IWellConstructionAdapter _constructionAdapter;
        private readonly IPerforationAdapter _perforationsAdapter;
        private readonly IAdapter<List<WellClass.Well.VskrutCl>?, ICollection<DrillingIn>> _stratigraphyAdapter;
        public WellAdapter(WellPropertiesConverter propertiesConverter,
            IAdapter<WellClass.Well.WorksCl?, ICollection<WellJob>> wellJobsAdapter,
            IAdapter<WellClass.HistoryClass[]?, ICollection<WellTypeChange>> wellTypeChangesAdapter,
            IWellConstructionAdapter constructionAdapter,
            IPerforationAdapter perforationsAdapter,
            IAdapter<List<WellClass.Well.VskrutCl>?, ICollection<DrillingIn>> stratigraphyAdapter)
        {
            _propertiesConverter = propertiesConverter ?? throw new ArgumentNullException(nameof(propertiesConverter));
            _wellJobsAdapter = wellJobsAdapter ?? throw new ArgumentNullException(nameof(wellJobsAdapter));
            _wellTypeChangesAdapter = wellTypeChangesAdapter ?? throw new ArgumentNullException(nameof(wellTypeChangesAdapter));
            _constructionAdapter = constructionAdapter ?? throw new ArgumentNullException(nameof(constructionAdapter));
            _perforationsAdapter = perforationsAdapter ?? throw new ArgumentNullException(nameof(perforationsAdapter));
            _stratigraphyAdapter = stratigraphyAdapter;
        }

        public Well Convert(WellClass.Well wellDto)
        {
            if (wellDto is null)
            {
                throw new ArgumentNullException(nameof(wellDto));
            }
            // TODO: нарушение, использую без инжекции сервис внутри 
            WellClass.Well.PerfCs[] openedIntervalsDto = OpenedIntervalsService.
                GetOpenedIntervals(wellDto.Perf, wellDto.Constr, wellDto.Charac.Zab.MD, DateTime.Now);

            var resultWell = new Well
            {
                Id = wellDto.UWI,
                Name = wellDto.Name,
                Lambda = wellDto.Charac.Lambda,
                // Charac, как написано, более-менее постоянные свойства
                Number = wellDto.Charac.Number.ToString(),
                Shape = _propertiesConverter.ConvertShape(wellDto.Charac.TypeStvol),
                // Конвертация!!!
                Area = wellDto.Charac.RazField,
                Region = wellDto.Charac.Rayon,
                PlantId = wellDto.Charac.UKPG.ToString(),
                WellGroup = wellDto.Charac.Kust,
                BalanceStartDate = wellDto.Charac.OnBalans,
                StartWorkDate = wellDto.Charac.StartWork,
                Altitude = wellDto.Charac.Alt,
                X = wellDto.Charac.X,
                Y = wellDto.Charac.Y,
                Water = wellDto.Charac.kWater,
                Roughness = wellDto.Charac.kRohness,
                BottomholeMd = wellDto.Charac.Zab?.MD ?? -999, // MMAIN.NullToDbN(Well.Zab.MD)
                BottomholeTvd = wellDto.Charac.Zab?.TVD ?? -999,
                BottomholeDx = wellDto.Charac.Zab?.DX ?? -999,
                BottomholeDy = wellDto.Charac.Zab?.DY ?? -999,
                DevelopmentObjectCalculated = wellDto.Charac.ObjRazr,
                DevelopmentObjectMain = wellDto.Charac.ObjRazr0,
                DevelopmentObjectAdditional = wellDto.Charac.ObjRazr1,
                // проектные--
                TypeByProject = _propertiesConverter.ConvertType(wellDto.Charac.TypeOnDr),
                BottomholeByProject = wellDto.Charac.Project?.Project_H ?? -999,
                HorizonByProject = wellDto.Charac.Project?.Project_Horizont ?? -999,
                Project = wellDto.Charac.Project?.Project_ID ?? -999,
                // Обратить внимание, другой класс! (вероятно, свойства, изменяющиеся во времени?)
                Type = _propertiesConverter.ConvertType(wellDto.Prop.TypeNow),
                State = _propertiesConverter.ConvertState(wellDto.Prop.StatNow),
                PerforationIntervalMiddleMd = wellDto.Prop.SIP?.MD ?? -999,
                PerforationIntervalMiddleTvd = wellDto.Prop.SIP?.TVD ?? -999,
                ReferencePlaneMd = wellDto.Prop.PLOSK?.MD ?? -999,
                ReferencePlandeTvd = wellDto.Prop.PLOSK?.TVD ?? -999,
                CurrentBottomholeMd = wellDto.Prop.ZabNow?.MD ?? -999,
                CurrentBottomholeTvd = wellDto.Prop.ZabNow?.TVD ?? -999,
                CurrentBottomholeDate = wellDto.Prop.ZabDate,
                ArtificialBottomholeMd = wellDto.Prop.ZabIskNow?.MD ?? -999,
                ArtificialBottomholeTvd = wellDto.Prop.ZabIskNow?.TVD ?? -999,
                WellTypeChanges = _wellTypeChangesAdapter.Convert(wellDto.Prop.Type),
                // Jobs
                WellJobs = _wellJobsAdapter.Convert(wellDto.works),
                //
                Construction = _constructionAdapter.Convert(wellDto.Constr),
                Perforations = _perforationsAdapter.Convert(wellDto.Perf, wellDto.Vskrut),
                // TODO: непосредственно без адаптера преобразую здесь + непосредственно использую сервис ReservoirPerforationService
                OpenedIntervals = openedIntervalsDto is null ? new List<OpenedInterval>() : openedIntervalsDto.Select(t => new OpenedInterval
                {
                    Top = t.Top,
                    Base = t.Baze,
                    DH = t.dH,
                    Reservoirs = ReservoirPerforationService.ReservoirsByInterval(wellDto.Vskrut, t.Top, t.Baze)
                }).ToList(),
                Stratigraphy = _stratigraphyAdapter.Convert(wellDto.Vskrut),
            };
            //TODO: сделать адаптеры по-нормальному
            if (wellDto.Prop?.Zab is not null)
            {
                resultWell.BottomholeHistory = wellDto.Prop.Zab.Select(t => new Bottomhole
                {
                    Id = t.ID,
                    Depth = t.Value,
                    Date = t.Dates,
                    Survey = t.Code
                }).ToList();
            }
            if (wellDto.Prop?.HydroHist is not null)
            {
                resultWell.HydrodynamicCharacteristicHistory = wellDto.Prop.HydroHist.Select(t => new HydrodynamicCharacteristic
                {
                    Id = t.IDIs,
                    Date = t.Dates,
                    IsBadSurvey = t.BadIssl,
                    A = t.A,
                    B = t.B,
                    E2s = t.E2s,
                    Theta = t.Tet
                }).ToList();
            }
            if (wellDto.Prop?.Contacts is not null)
            {
                resultWell.Contacts = wellDto.Prop.Contacts.Select(t => new WellContact
                {
                    Id = t.id,
                    WasSurvey = t.IDISSL > 0,
                    ContactType = t.Type,
                    Date = t.Dates,
                    Top = t.Top,
                    Base = t.Baze,
                    TopAbsolute = t.AOTop,
                    BaseAbsolute = t.AOBaze,
                    Temperature = t.T,
                    Reservoir = t.Plast,
                    Remark = t.Note
                }).ToList();
            }

            return resultWell;
        }

        public WellClass.Well ConvertBack(string fieldId, Well well, IEnumerable<EquipmentType> equipmentTypes)
        {
            var wellDto = new WellClass.Well
            {
                UWI = well.Id,
                Name = well.Name,

                //// Обратить внимание, другой класс! (вероятно, свойства, изменяющиеся во времени?)
                //Type  _propertiesConverter.ConvertType(wellDto.Prop.TypeNow) = well.,
                //State  _propertiesConverter.ConvertState(wellDto.Prop.StatNow) = well.,
                //PerforationIntervalMiddleMd  wellDto.Prop.SIP?.MD ?? -999 = well.,
                //PerforationIntervalMiddleTvd  wellDto.Prop.SIP?.TVD ?? -999 = well.,
                //ReferencePlaneMd  wellDto.Prop.PLOSK?.MD ?? -999 = well.,
                //ReferencePlandeTvd  wellDto.Prop.PLOSK?.TVD ?? -999 = well.,
                //CurrentBottomholeMd  wellDto.Prop.ZabNow?.MD ?? -999 = well.,
                //CurrentBottomholeTvd  wellDto.Prop.ZabNow?.TVD ?? -999 = well.,
                //CurrentBottomholeDate  wellDto.Prop.ZabDate = well.,
                //ArtificialBottomholeMd  wellDto.Prop.ZabIskNow?.MD ?? -999 = well.,
                //ArtificialBottomholeTvd  wellDto.Prop.ZabIskNow?.TVD ?? -999 = well.,
                Charac = new WellClass.Well.CharacCs
                {
                    Number = int.Parse(well.Number),
                    Name = well.Name.Trim(),
                    TypeStvol = _propertiesConverter.ConvertShapeBack(well.Shape),
                    Lambda = well.Lambda,
                    Field = int.Parse(fieldId),
                    // Конвертация!!!
                    RazField = well.Area,
                    Rayon = well.Region,
                    UKPG = int.Parse(well.PlantId),
                    Kust = well.WellGroup,
                    OnBalans = well.BalanceStartDate,
                    StartWork = well.StartWorkDate,
                    Alt = well.Altitude,
                    X = well.X,
                    Y = well.Y,
                    kWater = well.Water,
                    kRohness = well.Roughness,
                    Zab = new WellClass.Well.InclinCs
                    {
                        MD = well.BottomholeMd,
                        TVD = well.BottomholeTvd,
                        DX = well.BottomholeDx, // ?? Надо ли?
                        DY = well.BottomholeDy // ?? Надо ли?
                    },
                    //ObjRazr = well.DevelopmentObjectCalculated,
                    ObjRazr0 = well.DevelopmentObjectMain,
                    ObjRazr1 = well.DevelopmentObjectAdditional,
                    TypeOnDr = _propertiesConverter.ConvertTypeBack(well.TypeByProject),
                    Project = new WellClass.Well.ProjectData
                    {
                        Project_H = well.BottomholeByProject,
                        Project_Horizont = well.HorizonByProject,
                        Project_ID = well.Project
                    }
                },
                works = _wellJobsAdapter.ConvertBack(well.WellJobs),
                Prop = new WellClass.Well.PropCs
                {
                    Type = _wellTypeChangesAdapter.ConvertBack(well.WellTypeChanges)
                },
                Constr = _constructionAdapter.ConvertBack(well.Construction, equipmentTypes),
                Perf = _perforationsAdapter.ConvertBack(well.Perforations),
                // TODO: непосредственное преобразование здесь! Притом что есть отдельный адаптер
                Incl = well.Trajectory?.Count > 0 ? well.Trajectory?.Select(t => new WellClass.Well.InclinCs
                {
                    MD = t.Md,
                    TVD = t.Tvd,
                    ANG = t.Angle,
                    Az = t.Azimuth,
                    DX = t.DX,
                    DY = t.DY
                })?.ToArray() : null,
                Vskrut = _stratigraphyAdapter.ConvertBack(well.Stratigraphy)
            };
            //TODO: сделать адаптеры по-нормальному
            if (well.BottomholeHistory is not null)
            {

                wellDto.Prop.Zab = well.BottomholeHistory.Select(t => new WellClass.HistoryClass
                {
                    ID = t.Id,
                    Value = t.Depth,
                    Dates = t.Date,
                    Code = t.Survey
                }).ToArray();
            }
            if (well.HydrodynamicCharacteristicHistory is not null)
            {
                wellDto.Prop.HydroHist = well.HydrodynamicCharacteristicHistory.Select(t => new WellClass.Well.PropCs.HydroPropClass
                {
                    IDIs = t.Id,
                    Dates = t.Date,
                    BadIssl = t.IsBadSurvey,
                    A = t.A,
                    B = t.B,
                    E2s = t.E2s,
                    Tet = t.Theta
                }).ToArray();
            }
            if (well.Contacts is not null)
            {
                wellDto.Prop.Contacts = well.Contacts.Select(t => new WellClass.Well.PropCs.ContaktClass
                {
                    id = t.Id,
                    Type = t.ContactType,
                    Dates = t.Date,
                    Top = t.Top,
                    Baze = t.Base,
                    AOTop = t.TopAbsolute,
                    AOBaze = t.BaseAbsolute,
                    T = t.Temperature,
                    Plast = t.Reservoir,
                    Note = t.Remark
                }).ToArray();
            }
            return wellDto;
        }
    }
}
