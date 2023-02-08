using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.DataAccess.Common;
using static WellClass.Well.IsslCs;

namespace Web_Prom.Core.Blazor.DataAccess.Surveys.Detailed
{
    public class WellSurveyAdapter : IAdapter<WellClass.Well.IsslCs, WellSurvey>
    {
        private readonly IAdapter<isslWorksCL[], ICollection<WellSurveyJob>> _wellJobsAdapter;

        public WellSurveyAdapter(IAdapter<isslWorksCL[], ICollection<WellSurveyJob>> wellJobsAdapter)
        {
            _wellJobsAdapter = wellJobsAdapter ?? throw new ArgumentNullException(nameof(wellJobsAdapter));
        }

        public WellSurvey Convert(WellClass.Well.IsslCs wellSurveyDto)
        {
            if (wellSurveyDto is null)
            {
                throw new ArgumentNullException(nameof(wellSurveyDto));
            }

            var wellSurvey = new WellSurvey
            {
                Id = wellSurveyDto.ID.ToString(),
                Name = !string.IsNullOrEmpty(wellSurveyDto.TypeStr) ? wellSurveyDto.TypeStr.PadRight(5) + wellSurveyDto.Finish.ToString("dd.MM.yy") : string.Empty,
                Type = wellSurveyDto.Type,
                DateStart = wellSurveyDto.Start,
                DateEnd = wellSurveyDto.Finish,
                Purpose = wellSurveyDto.Цель,
                Result = wellSurveyDto.Результат,
                Recommendations = wellSurveyDto.Рекомендации,
                Automobile = wellSurveyDto.Automobil?.Trim() ?? string.Empty,
                Jobs = _wellJobsAdapter.Convert(wellSurveyDto.isslWorks),
                Remark = wellSurveyDto.Note
            };

            if (wellSurveyDto.Shabl is not null)
            {
                wellSurvey.Templating = new Templating
                {
                    Diameter = wellSurveyDto.Shabl.D,
                    TemplateStop = wellSurveyDto.Shabl.Hstop,
                    CurrentBottomhole = wellSurveyDto.Shabl.ZaboyNow,
                    Remark = wellSurveyDto.Shabl.Note
                };
            }

            // TODO: Сделать адаптер рабочих параметров
            if (wellSurveyDto.WorkPar is not null)
            {
                foreach (WorkParCs workPar in wellSurveyDto.WorkPar)
                {
                    wellSurvey.WorkingParameters.Add(new WorkingParametersItem
                    {
                        Id = workPar.ID,
                        Date = workPar.OnDate,
                        WellheadPressure = workPar.Pbuf,
                        ManifoldPressure = workPar.Pman,
                        WellheadTemperature = workPar.Tust,
                        AnnularPressure = workPar.Pzatr,
                        Flowrate = workPar.Q,
                        CasingPressureTop = workPar.Pmkv,
                        CasingPressureBase = workPar.Pmkn,
                        LiquidLevel = workPar.HLiq,
                        WaterLevel = workPar.Hwater,
                        Remark = workPar.Note
                    });
                }
            }

            // TODO: сделать адаптер ГКИ
            if (wellSurveyDto.GKI is not null)
            {
                wellSurvey.GasCondensateSurvey = new GasCondensateSurvey
                {
                    ReservoirGasDensity = wellSurveyDto.GKI.RoPlast,
                    CriticalPressure = wellSurveyDto.GKI.Pkr,
                    CriticalTemperature = wellSurveyDto.GKI.Tkr,
                    SeparationGasMoleFraction = wellSurveyDto.GKI.Mg_sep,
                    DryGasMoleFraction = wellSurveyDto.GKI.Mg_dry,
                    PC5Reservoir = wellSurveyDto.GKI.PC5_pl,
                    PC5Separation = wellSurveyDto.GKI.PC5_sep,
                    PC5Dry = wellSurveyDto.GKI.PC5_dry,
                    MReservoir = wellSurveyDto.GKI.M_pl,
                    MC5 = wellSurveyDto.GKI.M_C5,
                    ShrinkCoefficient = wellSurveyDto.GKI.Kus,
                    N2 = wellSurveyDto.GKI.N2,
                    CO2 = wellSurveyDto.GKI.CO2,
                    Remark = wellSurveyDto.GKI.Note
                };
            }

            // TODO: сделать адаптер статики
            if (wellSurveyDto.Stat is not null)
            {
                wellSurvey.StaticSurvey = new StaticSurvey
                {
                    ReservoirPressureCalculationMethod = wellSurveyDto.Stat.TypePpl, //(wellSurveyDto.Stat.TypePpl - 503400) / 10, // TODO: Убрать - костыль, чтобы получить 1, 2, 3 для отображения в комбобоксе
                    ReservoirPressurePerforationMiddle = wellSurveyDto.Stat.Ppl_perf,
                    ReservoirPressureReferencePlane = wellSurveyDto.Stat.Ppl_plosk,
                    WellheadPressure = wellSurveyDto.Stat.Pust,
                    AnnulusPresure = wellSurveyDto.Stat.Pzatr,
                    DepthPressure = wellSurveyDto.Stat.Pglib,
                    Depth = wellSurveyDto.Stat.Hglub,
                    IsOff = wellSurveyDto.Stat.OFF,
                    CasingPressureBase = wellSurveyDto.Stat.Pmkn,
                    CasingPressureTop = wellSurveyDto.Stat.Pmkv,
                    LiquidLevel = wellSurveyDto.Stat.HLiquid,
                    WaterLevel = wellSurveyDto.Stat.Hwater,
                    Remark1 = wellSurveyDto.Stat.Note,
                    Remark2 = wellSurveyDto.Stat.Note2 
                };
            }

            // TODO: сделать адаптер глубинки
            if (wellSurveyDto.Glub is not null)
            {
                wellSurvey.DeepSurvey = new DeepSurvey
                {
                    Remark = wellSurveyDto.Glub.Note,
                    TemperatureAtm = wellSurveyDto.Glub.Tatm,
                    LoadDiameter = wellSurveyDto.Glub.Dgr,
                    LoadLength = wellSurveyDto.Glub.Lgr,
                    DeepSurveyStations = wellSurveyDto.Glub.Points?.Select(t => Convert(wellSurveyDto.Glub, t))?.ToList() ?? new List<DeepSurveyStation>()
                };
            }

            // TODO: сделать адаптер ГДИ
            if (wellSurveyDto.Din is not null)
            {
                wellSurvey.GasdynamicAndNadymSurvey = new GasdynamicAndNadymSurvey
                {
                    Id = wellSurveyDto.Din.ID,
                    TypeQ = wellSurveyDto.Din.TypeQ,
                    BottomholePressureCalculationMethod = wellSurveyDto.Din.TypePzab,
                    NoAdiab = wellSurveyDto.Din.NoAdiab,
                    IsBad = wellSurveyDto.Din.BadIssl,
                    DictDiameter = wellSurveyDto.Din.Ddict,
                    A = wellSurveyDto.Din.A,
                    B = wellSurveyDto.Din.B,
                    C = wellSurveyDto.Din.C,
                    Theta = wellSurveyDto.Din.Tet,
                    E2s = wellSurveyDto.Din.e2s,
                    Exponent = wellSurveyDto.Din.PokStep,
                    Remark = wellSurveyDto.Din.Note,
                    Qc = wellSurveyDto.Din.Qc,
                    Qac = wellSurveyDto.Din.Qac,
                    APipesim = wellSurveyDto.Din.PS_A,
                    BPipesim = wellSurveyDto.Din.PS_B,
                    PSAmes = wellSurveyDto.Din.PS_Ames,
                    PSBmes = wellSurveyDto.Din.PS_Bmes,
                    GasDynamicRegimes = wellSurveyDto.Din.RejimList?.Select(Convert)?.ToList() ?? new List<GasDynamicRegime>()
                };
            }

            // TODO: сделать адаптер ГИС
            if (wellSurveyDto.GIS is not null)
            {
                wellSurvey.GeophysicalSurvey = new GeophysicalSurvey
                {
                    Stopping = wellSurveyDto.GIS.Stoping,
                    CurrentBottomhole = wellSurveyDto.GIS.ZabNow,
                    CementBridge = wellSurveyDto.GIS.Bridge,
                    Contacts = (wellSurveyDto.GIS.Contacts ?? Array.Empty<WellClass.Well.PropCs.ContaktClass>()).Select(Convert).ToList(),
                    Construction = (wellSurveyDto.GIS.Constr ?? Array.Empty<GISClass.ConstractionCl>()).Select(Convert).ToList(),
                    PerforationIntervals = (wellSurveyDto.GIS.Perfor ?? Array.Empty<GISClass.GisPerforCl>()).Select(Convert).ToList(),
                    InflowIntervals = (wellSurveyDto.GIS.Interval ?? Array.Empty<GISClass.IntervalClass>()).Select(Convert).ToList(),
                    ACC = (wellSurveyDto.GIS.AKC ?? Array.Empty<GISClass.AKCClass>()).Select(Convert).ToList()
                };
            }

            if(wellSurveyDto.KVD is not null)
            {
                wellSurvey.Pbu = new PressureBuildUpCurve
                {
                    A = wellSurveyDto.KVD.a,
                    B = wellSurveyDto.KVD.b,
                    Depth = wellSurveyDto.KVD.Hglub,
                    Points = wellSurveyDto.KVD.Points?.Select(Convert)?.ToList() ?? new List<BuildUpCurvePoint>(),
                    Remark = wellSurveyDto.KVD.Note
                };
            }
            // См. WorkPar - по-разному реализовано
            if(wellSurveyDto.Chemic is not null)
            {
                wellSurvey.HydrochemicalAnalysis = wellSurveyDto.Chemic.Select(Convert).ToList();
            }

            return wellSurvey;
        }

        public WellClass.Well.IsslCs ConvertBack(WellSurvey wellSurvey)
        {
            if (wellSurvey is null)
            {
                throw new ArgumentNullException(nameof(wellSurvey));
            }

            var wellSurveyDto = new WellClass.Well.IsslCs
            {
                ID = int.Parse(wellSurvey.Id),
                Type = wellSurvey.Type,
                Start = wellSurvey.DateStart,
                Finish = wellSurvey.DateEnd,
                Цель = wellSurvey.Purpose,
                Результат = wellSurvey.Result,
                Рекомендации = wellSurvey.Recommendations,
                Automobil = wellSurvey.Automobile,
                isslWorks = _wellJobsAdapter.ConvertBack(wellSurvey.Jobs),
                Note = wellSurvey.Remark
            };

            if (wellSurvey.Templating is not null)
            {
                wellSurveyDto.Shabl = new ShablonCl
                {
                    D = wellSurvey.Templating.Diameter,
                    Hstop = wellSurvey.Templating.TemplateStop,
                    ZaboyNow = wellSurvey.Templating.CurrentBottomhole,
                    Note = wellSurvey.Templating.Remark
                };
            }

            // TODO: Сделать адаптер рабочих параметров
            wellSurveyDto.WorkPar = wellSurvey.WorkingParameters?.Select(t => new WorkParCs
            {
                ID = t.Id == -999 ? default : t.Id, // TODO: костыль - теперь не передаём на сервер -999, разобраться, возможно удалить присвоение -999 в UI и на других слоях
                HLiq = t.LiquidLevel,
                Hwater = t.WaterLevel,
                Pmkv = t.CasingPressureTop,
                Pmkn = t.CasingPressureBase,
                OnDate = t.Date,
                Pbuf = t.WellheadPressure,
                Pman = t.ManifoldPressure,
                Tust = t.WellheadTemperature,
                Pzatr = t.AnnularPressure,
                Q = t.Flowrate,
                Note = t.Remark
            })?.ToArray();

            // TODO: сделать адаптер ГКИ
            if (wellSurvey.GasCondensateSurvey is not null)
            {
                wellSurveyDto.GKI = new GKIclass
                {
                    RoPlast = wellSurvey.GasCondensateSurvey.ReservoirGasDensity,
                    Pkr = wellSurvey.GasCondensateSurvey.CriticalPressure,
                    Tkr = wellSurvey.GasCondensateSurvey.CriticalTemperature,
                    Mg_sep = wellSurvey.GasCondensateSurvey.SeparationGasMoleFraction,
                    Mg_dry = wellSurvey.GasCondensateSurvey.DryGasMoleFraction,
                    PC5_pl = wellSurvey.GasCondensateSurvey.PC5Reservoir,
                    M_pl = wellSurvey.GasCondensateSurvey.MReservoir,
                    M_C5 = wellSurvey.GasCondensateSurvey.MC5,
                    Kus = wellSurvey.GasCondensateSurvey.ShrinkCoefficient,
                    N2 = wellSurvey.GasCondensateSurvey.N2,
                    CO2 = wellSurvey.GasCondensateSurvey.CO2,
                    Note = wellSurvey.GasCondensateSurvey.Remark
                };
            }

            // TODO: сделать адаптер статики
            if (wellSurvey.StaticSurvey is not null)
            {
                wellSurveyDto.Stat = new StatCs
                {
                    TypePpl = wellSurvey.StaticSurvey.ReservoirPressureCalculationMethod, // wellSurvey.StaticSurvey.ReservoirPressureCalculationMethod * 10 + 503400, // TODO: Убрать - костыль, чтобы получить 1, 2, 3 для отображения в комбобоксе
                    Ppl_perf = wellSurvey.StaticSurvey.ReservoirPressurePerforationMiddle,
                    Ppl_plosk = wellSurvey.StaticSurvey.ReservoirPressureReferencePlane,
                    Pust = wellSurvey.StaticSurvey.WellheadPressure,
                    Pzatr = wellSurvey.StaticSurvey.AnnulusPresure,
                    Pglib = wellSurvey.StaticSurvey.DepthPressure,
                    Hglub = wellSurvey.StaticSurvey.Depth,
                    OFF = wellSurvey.StaticSurvey.IsOff,
                    Pmkn = wellSurvey.StaticSurvey.CasingPressureBase,
                    Pmkv = wellSurvey.StaticSurvey.CasingPressureTop,
                    HLiquid = wellSurvey.StaticSurvey.LiquidLevel,
                    Hwater = wellSurvey.StaticSurvey.WaterLevel,
                    Note = wellSurvey.StaticSurvey.Remark1,
                    Note2 = wellSurvey.StaticSurvey.Remark2

                };
            }

            // TODO: сделать адаптер глубинки
            if (wellSurvey.DeepSurvey is not null)
            {
                wellSurveyDto.Glub = new GlubClass
                {
                    Note = wellSurvey.DeepSurvey.Remark.Trim(),
                    Tatm = wellSurvey.DeepSurvey.TemperatureAtm,
                    Dgr = wellSurvey.DeepSurvey.LoadDiameter,
                    Lgr = wellSurvey.DeepSurvey.LoadLength,
                    Points = wellSurvey.DeepSurvey.DeepSurveyStations.Select(t => ConvertBack(t)).ToArray()
                };
            }

            // TODO: сделать адаптер ГДИ
            if (wellSurvey.GasdynamicAndNadymSurvey is not null)
            {
                wellSurveyDto.Din = new DinCs
                {
                    ID = wellSurvey.GasdynamicAndNadymSurvey.Id ,
                    TypeQ = wellSurvey.GasdynamicAndNadymSurvey.TypeQ ,
                    TypePzab = wellSurvey.GasdynamicAndNadymSurvey.BottomholePressureCalculationMethod,
                    NoAdiab = wellSurvey.GasdynamicAndNadymSurvey. NoAdiab,
                    BadIssl = wellSurvey.GasdynamicAndNadymSurvey.IsBad ,
                    Ddict = wellSurvey.GasdynamicAndNadymSurvey.DictDiameter ,
                    A = wellSurvey.GasdynamicAndNadymSurvey.A,
                    B = wellSurvey.GasdynamicAndNadymSurvey.B,
                    C = wellSurvey.GasdynamicAndNadymSurvey.C,
                    Tet = wellSurvey.GasdynamicAndNadymSurvey.Theta,
                    e2s = wellSurvey.GasdynamicAndNadymSurvey.E2s,
                    PokStep = wellSurvey.GasdynamicAndNadymSurvey.Exponent,
                    Note = wellSurvey.GasdynamicAndNadymSurvey.Remark,
                    Qc = wellSurvey.GasdynamicAndNadymSurvey.Qc,
                    Qac = wellSurvey.GasdynamicAndNadymSurvey.Qac,
                    PS_A = wellSurvey.GasdynamicAndNadymSurvey.APipesim,
                    PS_B = wellSurvey.GasdynamicAndNadymSurvey.BPipesim,
                    PS_Ames = wellSurvey.GasdynamicAndNadymSurvey.PSAmes,
                    PS_Bmes = wellSurvey.GasdynamicAndNadymSurvey.PSBmes,
                    RejimList = wellSurvey.GasdynamicAndNadymSurvey.GasDynamicRegimes?.Select(ConvertBack)?.ToArray() ?? Array.Empty<DinCs.RejimClass>()
                };
            }

            // TODO: сделать адаптер ГИС
            if (wellSurvey.GeophysicalSurvey is not null)
            {
                wellSurveyDto.GIS = new GISClass
                {
                    Stoping = wellSurvey.GeophysicalSurvey.Stopping,
                    ZabNow = wellSurvey.GeophysicalSurvey.CurrentBottomhole,
                    Bridge = wellSurvey.GeophysicalSurvey.CementBridge,
                    Contacts = wellSurvey.GeophysicalSurvey.Contacts?.Select(ConvertBack)?.ToArray() ?? Array.Empty<WellClass.Well.PropCs.ContaktClass>(),
                    Constr = wellSurvey.GeophysicalSurvey.Construction?.Select(ConvertBack)?.ToArray() ?? Array.Empty<GISClass.ConstractionCl>(),
                    Perfor = wellSurvey.GeophysicalSurvey.PerforationIntervals?.Select(ConvertBack)?.ToArray() ?? Array.Empty<GISClass.GisPerforCl>(),
                    Interval = wellSurvey.GeophysicalSurvey.InflowIntervals?.Select(ConvertBack)?.ToArray() ?? Array.Empty<GISClass.IntervalClass>(),
                    AKC = wellSurvey.GeophysicalSurvey.ACC?.Select(ConvertBack)?.ToArray() ?? Array.Empty<GISClass.AKCClass>()
                };
            }

            if(wellSurvey.Pbu is not null)
            {
                wellSurveyDto.KVD = new KVDClass
                {
                    a = wellSurvey.Pbu.A,
                    b = wellSurvey.Pbu.B,
                    Hglub = wellSurvey.Pbu.Depth,
                    Note = wellSurvey.Pbu.Remark,
                    Points = wellSurvey.Pbu.Points?.Select(ConvertBack)?.ToArray() ?? Array.Empty<KVDClass.PointClass>()
                };
            }

            wellSurveyDto.Chemic = wellSurvey.HydrochemicalAnalysis.Select(ConvertBack).ToArray();

            return wellSurveyDto;

        }

        // TODO: вынести в адаптер глубинки
        private DeepSurveyStation Convert(GlubClass deepSurveyDto, GlubClass.GlubPointClass deepSurveyStationDto)
        {
            if (deepSurveyStationDto is null)
            {
                throw new ArgumentNullException(nameof(deepSurveyStationDto));
            }

            var resultStation = new DeepSurveyStation
            {
                Id = deepSurveyStationDto.ID,
                HeightMeasured = deepSurveyStationDto.Hz,
                HeightWellbore = deepSurveyStationDto.Hs,
                HeightVertical = deepSurveyStationDto.Hv,
                HeightAbsolute = deepSurveyStationDto.HAO,
                Pressure = deepSurveyStationDto.P,
                Temperature = deepSurveyStationDto.T,
                Density = (float)Math.Round(deepSurveyDto.po(deepSurveyStationDto), 3),
                Liquid = deepSurveyStationDto.Li,
                Remark = deepSurveyStationDto.Note
            };

            return resultStation;
        }

        // TODO: вынести в адаптер ГДИ
        private GasDynamicRegime Convert(DinCs.RejimClass regimeDto)
        {
            if (regimeDto is null)
            {
                throw new ArgumentNullException(nameof(regimeDto));
            }

            var resultRegime = new GasDynamicRegime
            {
                IdGasdynamicSurvey = regimeDto.IDGDI,
                IdRegime = regimeDto.IDrejim,
                UseSeparator = regimeDto.UseSeparator,
                RegimeIndex = regimeDto.NRegim,
                OnDate = regimeDto.OnDate,
                RegimeDuration = regimeDto.RegimLength,
                DiaphragmDiameter = regimeDto.Ddiaph,
                BufferPressure = regimeDto.Pbuf,
                ManifoldPressure = regimeDto.Pgsk,
                DictPressure = regimeDto.Pdikt,
                AnnulusPressure = regimeDto.Pzatr,
                BufferTemperature = regimeDto.Tbuf,
                DictTemperature = regimeDto.Tdikt,
                ReservoirPressure = regimeDto.Pzab,
                DepthPressure = regimeDto.Pglub,
                DepthBase = regimeDto.Hglub,
                ReservoirDepression = regimeDto.dP,
                ReservoirDepressionSquare = regimeDto.dP2,
                FlowrateDeterminationMethod = (FlowrateDeterminationMethod)regimeDto.typeQ,
                Flowrate = regimeDto.Q,
                IsOff = regimeDto.OFF,
                CasingPressureBase = regimeDto.Pmkn,
                CasingPressureTop = regimeDto.Pmkv,
                LiquidLevel = regimeDto.HLiq,
                WaterLevel = regimeDto.Hwater,
                ReservoirPressureDeterminationMethod = regimeDto.TypePzab,
                TubingMode = regimeDto.TubingMode,
                Remark = regimeDto.Note,

                UniformityCoefficient = regimeDto.Codn,

                TempId = regimeDto.TempID,
                Lambda = regimeDto.Lam,

                Qwater = regimeDto.Qwater,
                Qsand = regimeDto.Qsand,
                RemarkNadym = regimeDto.NoteNadum,
                GSU = (regimeDto.GSU ?? Array.Empty<DinCs.RejimClass.GSU_Cl>()).Select(Convert).ToList(),
                Tank = regimeDto.Tank
            };

            return resultRegime;
        }

        // TODO: вынести в отдельный адаптер 
        private GSU Convert(DinCs.RejimClass.GSU_Cl gsuDto)
        {
            if (gsuDto is null)
            {
                throw new ArgumentNullException(nameof(gsuDto));
            }

            var resultGSU = new GSU
            {
                Id = gsuDto.ID,
                Secs = gsuDto.Secs,
                Vwat = gsuDto.Vwat,
                Vmp = gsuDto.Vmp,
                C5Saving = gsuDto.BackupC5,
                Remark = gsuDto.Note
            };

            return resultGSU;
        }

        // TODO: вынести в отдельный адаптер 
        private Contact Convert(WellClass.Well.PropCs.ContaktClass contactDto)
        {
            if (contactDto is null)
            {
                throw new ArgumentNullException(nameof(contactDto));
            }

            var resultContact = new Contact
            {
                Id = contactDto.id,
                SurveyId = contactDto.IDISSL,
                Type = contactDto.Type,
                Top = contactDto.Top,
                Base = contactDto.Baze,
                Date = contactDto.Dates,
                Reservoir = contactDto.Plast,
                Temperature = contactDto.T,
                AbsoluteTop = contactDto.AOTop,
                AbsoluteBase = contactDto.AOBaze,
                Remark = contactDto.Note
            };

            return resultContact;
        }

        // TODO: вынести в отдельный адаптер 
        private ConstructionItem Convert(GISClass.ConstractionCl constructionDto)
        {
            if (constructionDto is null)
            {
                throw new ArgumentNullException(nameof(constructionDto));
            }

            var resultConstruction = new ConstructionItem
            {
                Id = constructionDto.ID,
                ConstructionElement = constructionDto.Cons,
                Top1 = constructionDto.Top1,
                Base1 = constructionDto.Base1,
                Top2 = constructionDto.Top2,
                Base2 = constructionDto.Base2,
                Diameter = constructionDto.D,
                InstallationPlace = constructionDto.Where,
                Remark = constructionDto.Note,
                AbsoluteTop2 = constructionDto.AO_top2,
                AbsoluteBase2 = constructionDto.AO_Base2,
                AbsoluteTop1 = constructionDto.AO_top1,
                AbsoluteBase1 = constructionDto.AO_Base1,
            };

            return resultConstruction;
        }

        // TODO: вынести в отдельный адаптер 
        private GeophysicalSurveyPerforation Convert(GISClass.GisPerforCl perforationDto)
        {
            if (perforationDto is null)
            {
                throw new ArgumentNullException(nameof(perforationDto));
            }

            var resultPerforation = new GeophysicalSurveyPerforation
            {
                Id = perforationDto.ID,
                Reservoir = perforationDto.Plast,
                Top1 = perforationDto.Top1,
                Base1 = perforationDto.Base1,
                Top2 = perforationDto.Top2,
                Base2 = perforationDto.Base2,
                Remark = perforationDto.Note,
                AbsoluteTop2 = perforationDto.AO_top2,
                AbsoluteBase2 = perforationDto.AO_Base2,
                AbsoluteTop1 = perforationDto.AO_top1,
                AbsoluteBase1 = perforationDto.AO_Base1,
            };

            return resultPerforation;
        }

        // TODO: вынести в отдельный адаптер 
        private InflowInterval Convert(GISClass.IntervalClass intervalDto)
        {
            if (intervalDto is null)
            {
                throw new ArgumentNullException(nameof(intervalDto));
            }

            // TODO: отображение признака перекрытия пласта цементным мостом - задействует issl.GIS.Bridge
            var resultInterval = new InflowInterval
            {
                Id = intervalDto.ID,
                Reservoir = intervalDto.Plast,
                Regime = intervalDto.Rej,
                Top = intervalDto.Top,
                Base = intervalDto.Base,
                InflowType = intervalDto.SteemType,
                HeightEffective = intervalDto.Hef,
                ReservoirProperties = intervalDto.FES,
                Flowrate = intervalDto.Q,
                Hydrofrac = intervalDto.GRP,
                Remark = intervalDto.Note,
                AbsoluteTop = intervalDto.AO_top,
                AbsoluteBase = intervalDto.AO_Base
            };

            return resultInterval;
        }

        private ACC Convert(GISClass.AKCClass akcDto)
        {
            if (akcDto is null)
            {
                throw new ArgumentNullException(nameof(akcDto));
            }

            var resultInterval = new ACC
            {
                Id = akcDto.ID,
                Top = akcDto.Top,
                Base = akcDto.Base,
                DH = akcDto.dH,
                CMCol = akcDto.CM_Col,
                CMFormation = akcDto.CM_Por,
                Backside = akcDto.Zatrub,
                Remark = akcDto.Note
            };

            return resultInterval;
        }

        private BuildUpCurvePoint Convert(KVDClass.PointClass pbuPointDto)
        {
            if (pbuPointDto is null)
            {
                throw new ArgumentNullException(nameof(pbuPointDto));
            }

            var result = new BuildUpCurvePoint
            {
                Id = pbuPointDto.ID,
                Pressure = pbuPointDto.Press,
                Time = pbuPointDto.Time
            };

            return result;
        }

        // TODO: вынести в отдельный адаптер 
        private HydrochemicalAnalysis Convert(ChemicCL hydrochemicalDto)
        {
            if (hydrochemicalDto is null)
            {
                throw new ArgumentNullException(nameof(hydrochemicalDto));
            }

            var resultHydrochemicalAnalysisItem = new HydrochemicalAnalysis
            {
                Id = hydrochemicalDto.ID,
                DateAn = hydrochemicalDto.DateAn,
                K = ConvertIon(hydrochemicalDto.K),
                Na = ConvertIon(hydrochemicalDto.Na),
                Ca = ConvertIon(hydrochemicalDto.Ca),
                Mg = ConvertIon(hydrochemicalDto.Mg),
                Sr = ConvertIon(hydrochemicalDto.Sr),
                NH4 = ConvertIon(hydrochemicalDto.NH4),
                Fe = ConvertIon(hydrochemicalDto.Fe),
                Cl = ConvertIon(hydrochemicalDto.Cl),
                SO4 = ConvertIon(hydrochemicalDto.SO4),
                HCO3 = ConvertIon(hydrochemicalDto.HCO3),
                CO3 = ConvertIon(hydrochemicalDto.CO3),
                H2PO4 = ConvertIon(hydrochemicalDto.H2PO4),
                B = ConvertIon(hydrochemicalDto.B),
                Br = ConvertIon(hydrochemicalDto.Br),
                I = ConvertIon(hydrochemicalDto.I),
                PH = hydrochemicalDto.pH,
                DryResidue = hydrochemicalDto.SuhoyOst,
                APAP = hydrochemicalDto.APAP,
                Vzvesh = hydrochemicalDto.Vzvesh,
                O2 = hydrochemicalDto.O2,
                UV_ob = hydrochemicalDto.UV_ob,
                UV_po = hydrochemicalDto.UV_po,
                VMV_po = hydrochemicalDto.VMV_po,
                VMV_СH3OH = hydrochemicalDto.VMV_СH3OH,
                Remark = hydrochemicalDto.Note,
                FluidTypeGeochemical = hydrochemicalDto.Fluid_Type_Geoxim,
                WT_Question = hydrochemicalDto.WT_Question,
                WT_K = hydrochemicalDto.WT_K,
                WT_T = hydrochemicalDto.WT_T,
                WT_PL = hydrochemicalDto.WT_PL,
                WT_PL_unknown = hydrochemicalDto.WT_PL_unknown,
                WT_PL_sen = hydrochemicalDto.WT_PL_sen,
                WT_PL_neo = hydrochemicalDto.WT_PL_neo,
                WT_PL_Ach = hydrochemicalDto.WT_PL_Ach,
                WT_C5 = hydrochemicalDto.WT_C5,
                WT_CH5OH = hydrochemicalDto.WT_CH5OH
            };

            return resultHydrochemicalAnalysisItem;
        }
        // TODO: вынести в отдельный адаптер
        private Ion ConvertIon(ChemicCL.IonCl ionDto)
        {
            return new Ion(ionDto.mm)
            {
                MilligramLiter = ionDto.mg_l
            };
        }

        //---------Обратные преобразования----------
        private GlubClass.GlubPointClass ConvertBack(DeepSurveyStation deepSurveyStation)
        {
            if (deepSurveyStation is null)
            {
                throw new ArgumentNullException(nameof(deepSurveyStation));
            }

            var resultStationDto = new GlubClass.GlubPointClass
            {
                ID = deepSurveyStation.Id == -999 ? default : deepSurveyStation.Id,
                Hz = deepSurveyStation.HeightMeasured,
                Hs = deepSurveyStation.HeightWellbore,
                Hv = deepSurveyStation.HeightVertical,
                HAO = deepSurveyStation.HeightAbsolute,
                P = deepSurveyStation.Pressure,
                T = deepSurveyStation.Temperature,
                //Density = (float)Math.Round(deepSurveyDto.po(deepSurveyStationDto), 3),
                Li = deepSurveyStation.Liquid,
                Note = deepSurveyStation.Remark.Trim()
            };

            return resultStationDto;
        }

        private DinCs.RejimClass ConvertBack(GasDynamicRegime regime)
        {
            if (regime is null)
            {
                throw new ArgumentNullException(nameof(regime));
            }

            var resultRegimeDto = new DinCs.RejimClass
            {
                IDGDI = regime.IdGasdynamicSurvey,
                IDrejim = regime.IdRegime == -999 ? default : regime.IdRegime, // TODO: костыль, избавиться, вероятно и не нужен, так как в режим не пишется -999 во время создания  нового - проверить
                UseSeparator = regime.UseSeparator,
                NRegim = regime.RegimeIndex,
                OnDate = regime.OnDate,
                RegimLength = regime.RegimeDuration,
                Ddiaph = regime.DiaphragmDiameter,
                Pbuf = regime.BufferPressure,
                Pgsk = regime.ManifoldPressure,
                Pdikt = regime.DictPressure,
                Pzatr = regime.AnnulusPressure,
                Tbuf = regime.BufferTemperature,
                Tdikt = regime.DictTemperature,
                Pzab = regime.ReservoirPressure,
                Pglub = regime.DepthPressure,
                Hglub = regime.DepthBase,
                dP = regime.ReservoirDepression,
                dP2 = regime.ReservoirDepressionSquare,
                typeQ = (DinCs.RejimClass.Qtype)regime.FlowrateDeterminationMethod,
                Q = regime.Flowrate,
                OFF = regime.IsOff,
                Pmkn = regime.CasingPressureBase,
                Pmkv = regime.CasingPressureTop,
                HLiq = regime.LiquidLevel,
                Hwater = regime.WaterLevel,
                TypePzab = regime.ReservoirPressureDeterminationMethod,
                TubingMode = regime.TubingMode,
                Note = regime.Remark,

                Codn = regime.UniformityCoefficient,

                TempID = regime.TempId,
                Lam = regime.Lambda,

                Qwater = regime.Qwater,
                Qsand = regime.Qsand,
                NoteNadum = regime.RemarkNadym,
                GSU = (regime.GSU ?? new List<GSU>()).Select(ConvertBack).ToArray(),
                Tank = regime.Tank
            };

            return resultRegimeDto;
        }

        private DinCs.RejimClass.GSU_Cl ConvertBack(GSU gsu)
        {
            if (gsu is null)
            {
                throw new ArgumentNullException(nameof(gsu));
            }

            var resultGsuDto = new DinCs.RejimClass.GSU_Cl
            {
                ID = gsu.Id == -999 ? default : gsu.Id, // TODO: избавиться
                Secs = gsu.Secs,
                Vwat = gsu.Vwat,
                Vmp = gsu.Vmp,
                BackupC5 = gsu.C5Saving,
                Note = gsu.Remark
            };

            return resultGsuDto;
        }

        private WellClass.Well.PropCs.ContaktClass ConvertBack(Contact contact)
        {
            if (contact is null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            var resultContact = new WellClass.Well.PropCs.ContaktClass
            {
                id = contact.Id == -999 ? default : contact.Id, // TODO: костыль - удалить
                IDISSL = contact.SurveyId,
                Type = contact.Type,
                Top = contact.Top,
                Baze = contact.Base,
                Dates = contact.Date,
                Plast = contact.Reservoir,
                T = contact.Temperature,
                AOTop = contact.AbsoluteTop,
                AOBaze = contact.AbsoluteBase,
                Note = contact.Remark
            };

            return resultContact;
        }

        private GISClass.ConstractionCl ConvertBack(ConstructionItem constructionItem)
        {
            if (constructionItem is null)
            {
                throw new ArgumentNullException(nameof(constructionItem));
            }

            var resultConstructionDto = new GISClass.ConstractionCl
            {
                ID = constructionItem.Id == -999 ? default : constructionItem.Id, // TODO: костыль - удалить
                Cons = constructionItem.ConstructionElement,
                Top1 = constructionItem.Top1,
                Base1 = constructionItem.Base1,
                Top2 = constructionItem.Top2,
                Base2 = constructionItem.Base2,
                D = constructionItem.Diameter,
                Where = constructionItem.InstallationPlace,
                Note = constructionItem.Remark,
                AO_top2 = constructionItem.AbsoluteTop2,
                AO_Base2 = constructionItem.AbsoluteBase2,
                AO_top1 = constructionItem.AbsoluteTop1,
                AO_Base1 = constructionItem.AbsoluteBase1,
            };

            return resultConstructionDto;
        }

        private GISClass.GisPerforCl ConvertBack(GeophysicalSurveyPerforation perforation)
        {
            if (perforation is null)
            {
                throw new ArgumentNullException(nameof(perforation));
            }

            var resultPerforationDto = new GISClass.GisPerforCl
            {
                ID = perforation.Id == -999 ? default : perforation.Id, // TODO: костыль - удалить
                Plast = perforation.Reservoir,
                Top1 = perforation.Top1,
                Base1 = perforation.Base1,
                Top2 = perforation.Top2,
                Base2 = perforation.Base2,
                Note = perforation.Remark,
                AO_top2 = perforation.AbsoluteTop2,
                AO_Base2 = perforation.AbsoluteBase2,
                AO_top1 = perforation.AbsoluteTop1,
                AO_Base1 = perforation.AbsoluteBase1,
            };

            return resultPerforationDto;
        }

        private GISClass.IntervalClass ConvertBack(InflowInterval interval)
        {
            if (interval is null)
            {
                throw new ArgumentNullException(nameof(interval));
            }

            // TODO: отображение признака перекрытия пласта цементным мостом - задействует issl.GIS.Bridge
            var resultIntervalDto = new GISClass.IntervalClass
            {
                ID = interval.Id == -999 ? default : interval.Id, // TODO: костыль - удалить
                Plast = interval.Reservoir,
                Rej = interval.Regime,
                Top = interval.Top,
                Base = interval.Base,
                SteemType = interval.InflowType,
                Hef = interval.HeightEffective,
                FES = interval.ReservoirProperties,
                Q = interval.Flowrate,
                GRP = interval.Hydrofrac,
                Note = interval.Remark,
                AO_top = interval.AbsoluteTop,
                AO_Base = interval.AbsoluteBase
            };

            return resultIntervalDto;
        }

        private GISClass.AKCClass ConvertBack(ACC acc)
        {
            if (acc is null)
            {
                throw new ArgumentNullException(nameof(acc));
            }

            var resultAkcDto = new GISClass.AKCClass
            {
                ID = acc.Id == -999 ? default : acc.Id, // TODO: костыль - удалить!
                Top = acc.Top,
                Base = acc.Base,
                dH = acc.DH,
                CM_Col = acc.CMCol,
                CM_Por = acc.CMFormation,
                Zatrub = acc.Backside,
                Note = acc.Remark
            };

            return resultAkcDto;
        }

        private KVDClass.PointClass ConvertBack(BuildUpCurvePoint buildUpCurvePoint)
        {
            if (buildUpCurvePoint is null)
            {
                throw new ArgumentNullException(nameof(buildUpCurvePoint));
            }

            var resultPointDto = new KVDClass.PointClass
            {
                ID = buildUpCurvePoint.Id == -999 ? default : buildUpCurvePoint.Id,
                Press = buildUpCurvePoint.Pressure,
                Time = buildUpCurvePoint.Time
            };

            return resultPointDto;
        }

        // TODO: вынести в отдельный адаптер 
        private ChemicCL ConvertBack(HydrochemicalAnalysis hydrochemicalAnalysisItem)
        {
            if (hydrochemicalAnalysisItem is null)
            {
                throw new ArgumentNullException(nameof(hydrochemicalAnalysisItem));
            }

            var resultHydrochemicalAnalysisDto = new ChemicCL
            {
                ID = hydrochemicalAnalysisItem.Id == -999 ? default : hydrochemicalAnalysisItem.Id, // TODO: костыль - удалить
                DateAn = hydrochemicalAnalysisItem.DateAn,
                K = ConvertIonBack(hydrochemicalAnalysisItem.K),
                Na = ConvertIonBack(hydrochemicalAnalysisItem.Na),
                Ca = ConvertIonBack(hydrochemicalAnalysisItem.Ca),
                Mg = ConvertIonBack(hydrochemicalAnalysisItem.Mg),
                Sr = ConvertIonBack(hydrochemicalAnalysisItem.Sr),
                NH4 = ConvertIonBack(hydrochemicalAnalysisItem.NH4),
                Fe = ConvertIonBack(hydrochemicalAnalysisItem.Fe),
                Cl = ConvertIonBack(hydrochemicalAnalysisItem.Cl),
                SO4 = ConvertIonBack(hydrochemicalAnalysisItem.SO4),
                HCO3 = ConvertIonBack(hydrochemicalAnalysisItem.HCO3),
                CO3 = ConvertIonBack(hydrochemicalAnalysisItem.CO3),
                H2PO4 = ConvertIonBack(hydrochemicalAnalysisItem.H2PO4),
                B = ConvertIonBack(hydrochemicalAnalysisItem.B),
                Br = ConvertIonBack(hydrochemicalAnalysisItem.Br),
                I = ConvertIonBack(hydrochemicalAnalysisItem.I),
                pH = hydrochemicalAnalysisItem.PH,
                SuhoyOst = hydrochemicalAnalysisItem.DryResidue,
                APAP = hydrochemicalAnalysisItem.APAP,
                Vzvesh = hydrochemicalAnalysisItem.Vzvesh,
                O2 = hydrochemicalAnalysisItem.O2,
                UV_ob = hydrochemicalAnalysisItem.UV_ob,
                UV_po = hydrochemicalAnalysisItem.UV_po,
                VMV_po = hydrochemicalAnalysisItem.VMV_po,
                VMV_СH3OH = hydrochemicalAnalysisItem.VMV_СH3OH,
                Note = hydrochemicalAnalysisItem.Remark,
                Fluid_Type_Geoxim = hydrochemicalAnalysisItem.FluidTypeGeochemical,
                WT_Question = hydrochemicalAnalysisItem.WT_Question,
                WT_K = hydrochemicalAnalysisItem.WT_K,
                WT_T = hydrochemicalAnalysisItem.WT_T,
                WT_PL = hydrochemicalAnalysisItem.WT_PL,
                WT_PL_unknown = hydrochemicalAnalysisItem.WT_PL_unknown,
                WT_PL_sen = hydrochemicalAnalysisItem.WT_PL_sen,
                WT_PL_neo = hydrochemicalAnalysisItem.WT_PL_neo,
                WT_PL_Ach = hydrochemicalAnalysisItem.WT_PL_Ach,
                WT_C5 = hydrochemicalAnalysisItem.WT_C5,
                WT_CH5OH = hydrochemicalAnalysisItem.WT_CH5OH
            };

            return resultHydrochemicalAnalysisDto;
        }
        // TODO: вынести в отдельный адаптер
        private ChemicCL.IonCl ConvertIonBack(Ion ion)
        {
            return new ChemicCL.IonCl(ion.MolarMass)
            {
               mg_l = ion.MilligramLiter
            };
        }
    } 
}
