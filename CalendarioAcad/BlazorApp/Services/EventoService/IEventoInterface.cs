using BlazorApp.Models;

namespace BlazorApp.Services.EventoService
{
    public interface IEventoInterface
    {
        public Task<List<Evento>> GetEventoPorCalendario(int idCalendario);
        public Task<Evento> GetEventoById(int idEvento);
        public Task<ResponseModel<Evento>> UpdateEvento(Evento evento);
        public Task<ResponseModel<Evento>> CriarEvento(Evento evento, int idCalendario);


    }
}
