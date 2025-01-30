using API.DTO.Eventos;
using API.Model;
using API.Services.Eventos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventoInterface _eventoInterface;
        public EventosController(IEventoInterface eventoInterface)
        {
            _eventoInterface = eventoInterface;
        }

        [HttpGet("ListarEventos")]
        public async Task<ActionResult<ResponseModel<List<Evento>>>> ListarEventos()
        {
            var eventos = await _eventoInterface.ListarEventos();
            return Ok(eventos);
        }

        [HttpGet("EventoPorId/{eventoId}")]
        public async Task<ActionResult<ResponseModel<Evento>>> EventoPorId(int eventoId)
        {
            var evento = await _eventoInterface.EventorPorID(eventoId);
            return Ok(evento);
        }

        [HttpPost("CriarEvento/{idCalendario}")]
        public async Task<ActionResult<ResponseModel<Evento>>> CriarEvento(CriarEventoDTO dados, int idCalendario)
        {
            var eventos = await _eventoInterface.CriarEvento(dados, idCalendario);
            return Ok(eventos);
        }

        [HttpPost("EditarEvento/{idEvento}")]
        public async Task<ActionResult<ResponseModel<Evento>>> EditarEvento(EditarEventoDTO editarEvento, int idEvento)
        {
            var eventos = await _eventoInterface.EditarEvento(editarEvento, idEvento);
            return Ok(eventos);
        }

        [HttpPost("DesativarEvento/{idEvento}")]
        public async Task<ActionResult<ResponseModel<Evento>>> DesativarEvento(int idEvento)
        {
            var evento = await _eventoInterface.DesativarEvento(idEvento);
            
            if(!evento.Status)
                return BadRequest(evento);
            
            return Ok(evento);
        }

        [HttpGet("EventoPorCalendario/{idCalendario}")]
        public async Task<ActionResult<ResponseModel<List<Evento>>>> EventoPorCalendario(int idCalendario)
        {
            var eventos = await _eventoInterface.EventoPorCalendario(idCalendario);
            return Ok(eventos);
        }
    }
}
