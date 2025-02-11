using BlazorApp.Models;
using BlazorApp.Models.DTO;

namespace BlazorApp.Services.PortariaService
{
    public interface IPortariaInterface
    {
        public Task<ResponseModel<Portaria>> CreatePortaria(int IdCalendario, List<Evento_Portaria> eventoPortarias);
        public Task<List<Portaria>> GetAllPortarias();

    }
}