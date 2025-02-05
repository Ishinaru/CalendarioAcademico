using BlazorApp.Models;
using BlazorApp.Models.DTO;
using System.Net.Http.Json;

namespace BlazorApp.Services.PortariaService
{
    public class PortariaService : IPortariaInterface
    {
        private readonly HttpClient _httpClient;
        private const string BaseURL = "/api/Portaria";

        public PortariaService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
        }
        public async Task<ResponseModel<Portaria>> CreatePortaria(int IdCalendario, CriarPortariaDTO portaria, EventoPortariasDTO eventoPortarias)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseURL}/CriarPortaria/{IdCalendario}", new { portaria, eventoPortarias });
            return await HandleResponseAsync<Portaria>(response);
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
