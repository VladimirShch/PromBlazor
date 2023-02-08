using Geolog.Contracts;
using Geolog.Wells.Trajectories;
using System.Data;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.ApplicationLayer.ReferenceInformation;
using Web_Prom.Core.Blazor.ApplicationLayer.Wells;
using Web_Prom.Core.Blazor.ApplicationLayer.Wells.Export;
using Web_Prom.Core.Blazor.Core.Entities.Horizons;
using Web_Prom.Core.Blazor.Core.Entities.Perforations;
using Web_Prom.Core.Blazor.Core.Entities.ReferenceInformation;
using Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures;
using Web_Prom.Core.Blazor.Core.Entities.Stratigraphy;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Detailed.Services;
using Web_Prom.Core.Blazor.Core.Entities.Surveys.Short;
using Web_Prom.Core.Blazor.Core.Entities.Trajectories;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.Core.Entities.WellJobs;
using Web_Prom.Core.Blazor.Core.Entities.Wells;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed;
using Web_Prom.Core.Blazor.Core.Entities.WellTypesHistory;
using Web_Prom.Core.Blazor.DataAccess.Common;
using Web_Prom.Core.Blazor.DataAccess.Credentials;
using Web_Prom.Core.Blazor.DataAccess.ReferenceInformation;
using Web_Prom.Core.Blazor.DataAccess.ReservoirPressures;
using Web_Prom.Core.Blazor.DataAccess.Stratigraphy;
using Web_Prom.Core.Blazor.DataAccess.Surveys.Detailed;
using Web_Prom.Core.Blazor.DataAccess.Surveys.Services;
using Web_Prom.Core.Blazor.DataAccess.Surveys.Short;
using Web_Prom.Core.Blazor.DataAccess.Surveys.SurveyJobs;
using Web_Prom.Core.Blazor.DataAccess.Trajectories;
using Web_Prom.Core.Blazor.DataAccess.WellConstructions;
using Web_Prom.Core.Blazor.DataAccess.WellJobs;
using Web_Prom.Core.Blazor.DataAccess.WellPerforations;
using Web_Prom.Core.Blazor.DataAccess.WellProjects;
using Web_Prom.Core.Blazor.DataAccess.Wells.Common;
using Web_Prom.Core.Blazor.DataAccess.Wells.Detailed;
using Web_Prom.Core.Blazor.DataAccess.Wells.Export;
using Web_Prom.Core.Blazor.DataAccess.Wells.Short;
using Web_Prom.Core.Blazor.DataAccess.WellTypesHistory;

namespace Web_Prom.Core.Blazor.DataAccess
{
    public class DataAccessInitializer
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            //--------!!!---------------------------
            services.AddScoped(s => new Communication.Client("https://localhost:44389", new HttpClient { BaseAddress = new Uri("https://localhost:44389") }));
            //--------!!!--------------------------
            services.AddScoped(s => Lib.GeologClient.Default.Get<IByArc>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<IPass>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<IWORK>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<ISprav>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<ITrajectoryAppService>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<IIssl>());
            services.AddScoped(s => Lib.GeologClient.Default.Get<IGrafic>());
            
            services.AddScoped<ICredentialsService, CredentialsService>();
            services.AddScoped<WellPropertiesConverter>();

            services.AddScoped<IWellExportService, WellExportService>();

            // Костыли *********************************
            //-- Статическая информация - заполняется раз при запуске программы--
            var wellRelatedStaticInfoRepository = new WellRelatedStaticInfoRepository();
            services.AddSingleton<IWellRelatedStaticInfoRepositoryMaker>(wellRelatedStaticInfoRepository);
            services.AddSingleton<IWellRelatedStaticInfoRepository>(wellRelatedStaticInfoRepository);

            services.AddSingleton<WellProjectRepository>();
            services.AddSingleton<IWellProjectRepository>(s => s.GetRequiredService<WellProjectRepository>());
            services.AddSingleton<IWellProjectRepositoryAndMaker>(s => s.GetRequiredService<WellProjectRepository>());
            //**********************************************

            // EntityServices
            services.AddScoped<IWellShortAdapter, WellShortAdapter>();
            services.AddScoped<IWellShortRepository, WellShortRepositoryClient>();
            //services.AddScoped<IWellShortRepository, WellShortRepository>(); // Костыль! - избавиться

            services.AddScoped<IAdapter<WellClass.Well.WorksCl?, ICollection<WellJob>>, WellJobsAdapter>();
            services.AddScoped<IAdapter<WellClass.HistoryClass[]?, ICollection<WellTypeChange>>, WellTypeChangesAdapter>();
            services.AddScoped<IWellConstructionAdapter, WellConstructionAdapter>();

            services.AddScoped<IPerforationAdapter, PerforationAdapter>();
            services.AddScoped<IWellPerforationRepository, WellPerforationRepository>();

            services.AddScoped<IAdapter<DataTable?, ICollection<EquipmentType>>, EquipmentTypesAdapter>();
            services.AddScoped<EquipmentTypeRepository>();
            services.AddScoped<IWellConstructionRepository, WellConstructionRepository>();

            services.AddScoped<IAdapter<IEnumerable<TrajectoryStationDto>?, ICollection<TrajectoryStation>>, TrajectoryAdapter>();
            services.AddScoped<ITrajectoryRepository, TrajectoryRepository>();

            services.AddScoped<IAdapter<List<WellClass.Well.VskrutCl>?, ICollection<DrillingIn>>, StratigraphyAdapter>();

            services.AddScoped<IWellAdapter, WellAdapter>();
            services.AddScoped<IWellRepository, WellRepository>();

            services.AddScoped<IAdapter<IEnumerable<WellClass.Well.IsslCs>?, ICollection<WellSurveyShort>>, WellSurveysShortAdapter>();
            services.AddScoped<IWellSurveysShortRepository, WellSurveysShortRepository>();

            services.AddScoped<IAdapter<WellClass.Well.IsslCs.isslWorksCL[], ICollection<WellSurveyJob>>, WellSurveyJobsAdapter>();
            services.AddScoped<IAdapter<WellClass.Well.IsslCs, WellSurvey>, WellSurveyAdapter>();
            services.AddScoped<IWellSurveyRepository, WellSurveyRepository>();

            services.AddScoped<ISurveysCalculationService, SurveysCalculationService>();
            services.AddScoped<ISurveysDeletionService, SurveysDeletionService>();
            //--------------Приведение пластовых давлений---------------
            services.AddScoped<IReservoirPressureInformationAdapter, ReservoirPressureInformationAdapter>();
            services.AddScoped<IReservoirPressureInformationRepository, ReservoirPressureInformationRepository>();

            //---------Костыли - находится тут, так как выполняется при запуске программы, а использует IAdapter<DataTable?, ICollection<EquipmentType>>, который уже должен быть зарегистрирован-------------
            // -- Динамическая информация - обновляется каждый раз, например, при открытии формы (справочник скважин, исследования и т.д.)
            services.AddSingleton<DynamicReferenceInformationRepository>();
            services.AddSingleton<IDynamicReferenceInformationRepository>(s => s.GetRequiredService<DynamicReferenceInformationRepository>());
            services.AddSingleton<IDynamicReferenceInformationRepositoryMaker>(s => s.GetRequiredService<DynamicReferenceInformationRepository>());
            services.AddSingleton<IDynamicReferenceInformationRepositoryAndMaker>(s => s.GetRequiredService<DynamicReferenceInformationRepository>());
            //------------------------------
        }
    }
}
