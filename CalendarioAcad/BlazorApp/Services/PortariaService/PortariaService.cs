using BlazorApp.Models;
using BlazorApp.Models.DTO;
using System.Net.Http.Json;

namespace BlazorApp.Services.PortariaService
{
    public class PortariaService(IHttpClientFactory httpClient)
    {
        public async Task<ResponseModel<Portaria>> CriarPortariaAsync(CriarPortariaEventosDTO dados)
        {
            var client = httpClient.CreateClient(Configuration.HttpClientName);
            var response = await client.PostAsJsonAsync($"/api/Portaria/CriarPortaria", dados);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseModel<Portaria>>();
                throw new Exception(errorResponse?.Mensagem);
            }

            return await response.Content.ReadFromJsonAsync<ResponseModel<Portaria>>();
        }
    }
}
