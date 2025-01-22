using BlazorApp.Models;
using BlazorApp.Models.DTO.Calendario;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
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

        public async Task ApproveAsync(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsync($"api/Calendarios/AprovarCalendario/{idCalendario}", null);
            
        }

        public async Task<Calendario> DesativarCalendario(int idCalendario)
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsJsonAsync($"/api/Calendarios/DesativarCalendario/{idCalendario}", idCalendario);
            return await response.Content.ReadFromJsonAsync<Calendario>();
        }
    }
}
