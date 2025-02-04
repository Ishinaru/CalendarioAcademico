using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp.Services.EventoService
{
    public class EventoService: IEventoInterface
    {
        private readonly HttpClient _httpClient;
        private const string BasePath = "/api/Eventos";
        public EventoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
        }
        public async Task<List<Evento>> GetEventoPorCalendario(int idCalendario)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/EventoPorCalendario/{idCalendario}");
            
            var result = await HandleResponseAsync<List<Evento>>(response);
            return result?.Dados ?? [];
        }

        public async Task<Evento> GetEventoById(int idEvento)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/EventoPorId/{idEvento}");

            var result = await HandleResponseAsync<Evento>(response);

            return result?.Dados;
        }

        public async Task<ResponseModel<Evento>> UpdateEvento(Evento evento)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BasePath}/EditarEvento/{evento.IdEvento}", evento);

            return await HandleResponseAsync<Evento>(response);
        }

        public async Task<ResponseModel<Evento>> CriarEvento(Evento evento, int idCalendario)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BasePath}/CriarEvento/{idCalendario}", evento);

            return await HandleResponseAsync<Evento>(response);
        }

        private async Task<ResponseModel<T>> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseModel<T>>();
                throw new HttpRequestException(errorResponse?.Mensagem ?? "Erro desconhecido");
            }

            return await response.Content.ReadFromJsonAsync<ResponseModel<T>>();
        }
    }
}
