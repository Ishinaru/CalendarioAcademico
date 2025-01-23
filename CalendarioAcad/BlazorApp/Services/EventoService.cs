using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class EventoService(IHttpClientFactory httpClientFactory)
    {
        public async Task<List<Evento>> GetEventoPorCalendario(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetFromJsonAsync<ResponseModel<List<Evento>>>($"/api/Eventos/EventoPorCalendario/{idCalendario}");
            return response?.Dados ?? [];
        }
    }
}
