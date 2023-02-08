using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;

namespace Web_Prom.Core.Blazor.ApplicationLayer
{
    public class ApplicationLayerInitializer
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            //services.AddSingleton(() => new UserCredentials());
            services.AddSingleton((s) => new UserCredentials { Username="V_Shulkin", Password="6124"});
        }
    }
}
