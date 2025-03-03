using BlazorApp.Services.CalendarioService;
using BlazorApp.Services.EventoService;
using BlazorApp.Services.PortariaService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient(
                Configuration.HttpClientName,
                x =>
                {
                    x.BaseAddress = new Uri(Configuration.BackendUrl);
                });

            builder.Services.AddScoped<ICalendarioInterface, CalendarioService>();
            builder.Services.AddScoped<IEventoInterface, EventoService>();
            builder.Services.AddScoped<IPortariaInterface, PortariaService>();

            builder.Services.AddBlazorBootstrap();

            await builder.Build().RunAsync();
        }
    }
}
