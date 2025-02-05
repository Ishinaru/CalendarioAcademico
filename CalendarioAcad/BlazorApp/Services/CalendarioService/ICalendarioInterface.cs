using BlazorApp.Models;
using BlazorApp.Models.DTO.Calendario;

namespace BlazorApp.Services.CalendarioService
{
    public interface ICalendarioInterface
    {
        public Task<ResponseModel<Calendario>> CreateCalendario(CriarCalendarioDTO calendario);

        public Task<List<Calendario>> GetAllCalendarios();

        public Task<Calendario?> GetCalendarioById(int idCalendario);

        public Task<List<Calendario>> GetCalendariosAprovados();

        public Task<ResponseModel<Calendario>> AprovarCalendario(int idCalendario);

        public Task<ResponseModel<Calendario>> DesativarCalendario(int idCalendario);
    }
}
