using BlazorApp.Models;
using BlazorApp.Models.DTO.Calendario;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp.Services.CalendarioService
{
    public class CalendarioService : ICalendarioInterface
    {
        private readonly HttpClient _httpClient;
        private const string BasePath = "api/Calendarios";
        
        public CalendarioService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
        }
        public async Task<ResponseModel<Calendario>> CreateCalendario(CriarCalendarioDTO calendario)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BasePath}/CriarCalendario", calendario);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ResponseModel<Calendario>>();
                throw new HttpRequestException($"Erro: {error}");
            }
            return await response.Content.ReadFromJsonAsync<ResponseModel<Calendario>>();
        }

        public async Task<List<Calendario>> GetAllCalendarios()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<Calendario>>>($"{BasePath}/ListarCalendarios");
            return response?.Dados ?? [];
        }

        public async Task<Calendario?> GetCalendarioById(int idCalendario)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<Calendario>>($"{BasePath}/CalendarioPorId/{idCalendario}");
            return response?.Dados;
        }

        public async Task<List<Calendario>> GetCalendariosAprovados()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<Calendario>>>($"{BasePath}/GetCalendariosAprovados");
            return response?.Dados ?? [];
        }

        public async Task<ResponseModel<Calendario>> AprovarCalendario(int idCalendario)
        {
            var response = await _httpClient.PatchAsync($"{BasePath}/AprovarCalendario/{idCalendario}", null);
            return await HandleResponse<Calendario>(response);
        }
        
        public async Task<ResponseModel<Calendario>> DesativarCalendario(int idCalendario)
        {
            var response = await _httpClient.PatchAsync($"{BasePath}/DesativarCalendario/{idCalendario}", null);
            return await HandleResponse<Calendario>(response);
        }

        private static async Task<ResponseModel<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Resquest fail: {error}");
            }
            return await response.Content.ReadFromJsonAsync<ResponseModel<T>>() ?? throw new InvalidOperationException("Invalid response format");
        }
 
    }
}
