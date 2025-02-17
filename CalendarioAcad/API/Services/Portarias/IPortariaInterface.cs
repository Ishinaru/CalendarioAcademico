using API.DTO.EventoPortarias;
using API.DTO.Portarias;
using API.Model;

namespace API.Services.Portarias
{
    public interface IPortariaInterface
    {
        Task<ResponseModel<List<Portaria>>> ListarPortarias();
        Task<ResponseModel<Portaria>> CriarPortaria(CriarPortariaDTO portariaDTO, List<EventoPortariasDTO> eventoPortariasDTO);
        //Task<ResponseModel<List<Portaria>>> EditarPortaria(EditarPortariaDTO editarPortaria, int idPortaria);
        Task<ResponseModel<Portaria>> DesativarPortaria(int idPortaria);
    }
}
