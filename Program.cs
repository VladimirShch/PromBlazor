using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebProm.Core.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<PromApp>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// CoreInitializer
Web_Prom.Core.Blazor.ApplicationLayer.ApplicationLayerInitializer.RegisterDependencies(builder.Services);
Web_Prom.Core.Blazor.DataAccess.DataAccessInitializer.RegisterDependencies(builder.Services);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
