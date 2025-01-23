using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.Intrinsics.X86;

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

            builder.Services.AddTransient<CalendarioService>();
            builder.Services.AddTransient<EventoService>();

            await builder.Build().RunAsync();
        }
    }
}
