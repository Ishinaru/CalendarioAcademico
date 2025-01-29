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

        public async Task<Evento> GetEventoById(int idEvento)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetFromJsonAsync<ResponseModel<Evento>>($"/api/Eventos/EventoPorId/{idEvento}");
            return response?.Dados;
        }
}
