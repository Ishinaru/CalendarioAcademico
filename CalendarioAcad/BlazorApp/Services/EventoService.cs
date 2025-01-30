using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
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

        public async Task <ResponseModel<Evento>> UpdateEvento(Evento evento)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsJsonAsync($"/api/Eventos/EditarEvento/{evento.IdEvento}", evento);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseModel<Evento>>();
                throw new Exception(errorResponse?.Mensagem);
            }

            return await response.Content.ReadFromJsonAsync<ResponseModel<Evento>>();
        }

        public async Task<ResponseModel<Evento>> CriarEvento(Evento evento, int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsJsonAsync($"/api/Eventos/CriarEvento/{idCalendario}", evento);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseModel<Evento>>();
                throw new Exception(errorResponse?.Mensagem);
            }
            
            return await response.Content.ReadFromJsonAsync<ResponseModel<Evento>>();
        }
    }
}
