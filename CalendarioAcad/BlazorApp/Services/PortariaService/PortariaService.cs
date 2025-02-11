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
        public async Task<List<Portaria>> GetAllPortarias()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<Portaria>>>($"{BaseURL}/ListarPortarias");
            return response?.Dados ?? [];
        }

        public async Task<ResponseModel<Portaria>> CreatePortaria(int IdCalendario, List<Evento_Portaria> eventoPortarias)
        {
            CriarPortariaDTO portariaDTO = new();

            var eventoPortariasDTO = eventoPortarias.Select(e => new EventoPortariasDTO
            {
                EventoID = e.EventoID,
                DataInicio = e.DataInicio,
                DataFinal = e.DataFinal,
                Observacao = e.Observacao
            }).ToList();

            var dados = new CriarPortariaEventosDTO
            {
                PortariaDTO = portariaDTO,
                EventoPortariasDTO = eventoPortariasDTO
            };

            var response = await _httpClient.PostAsJsonAsync($"{BaseURL}/CriarPortaria", dados);
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
