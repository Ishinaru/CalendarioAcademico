using API.DTO.Eventos;
using API.Model;

namespace API.Services.Eventos
{
    public interface IEventoInterface
    {
        Task<ResponseModel<List<Evento>>> ListarEventos();
        Task<ResponseModel<Evento>> EventorPorID(int idEvento);
        Task<ResponseModel<Evento>> CriarEvento(CriarEventoDTO eventoDTO, int idCalendario);
        Task<ResponseModel<Evento>> EditarEvento(EditarEventoDTO editarEvento, int idEvento);
        Task<ResponseModel<Evento>> DesativarEvento(int idEvento);
        Task<ResponseModel<List<Evento>>> EventoPorCalendario (int idCalendario);
    }
}
