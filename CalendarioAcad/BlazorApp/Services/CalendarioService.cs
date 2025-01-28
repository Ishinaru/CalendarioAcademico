using BlazorApp.Models;
using BlazorApp.Models.DTO.Calendario;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class CalendarioService(IHttpClientFactory httpClientFactory)
    {
        public async Task CreateAsync(CriarCalendarioDTO calendario, NavigationManager navManager)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsJsonAsync("api/Calendarios/CriarCalendario", calendario);
            if (response.IsSuccessStatusCode)
            {
                navManager.NavigateTo("/Calendario/ViewCalendario");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
            }
        }
        public async Task<List<Calendario>> GetAsync()
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetFromJsonAsync<ResponseModel<List<Calendario>>>("api/Calendarios/ListarCalendarios");
            return response?.Dados ?? [];
        }

        public async Task<Calendario> GetByIdAsync(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetFromJsonAsync<ResponseModel<Calendario>>($"api/Calendarios/CalendarioPorId/{idCalendario}");
            return response?.Dados;
        }
        public async Task<Calendario> AprovarCalendario(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PatchAsync($"/api/Calendarios/AprovarCalendario/{idCalendario}", null);
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<Calendario>>();
            return responseModel?.Dados;
        }
        public async Task<Calendario> DesativarCalendario(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PatchAsync($"/api/Calendarios/DesativarCalendario/{idCalendario}", null);
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<Calendario>>();
            return responseModel?.Dados;
        }
    }
}
