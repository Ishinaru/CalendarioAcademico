using API.DTO.Calendarios;
using API.Model;
using API.Services.Calendarios;
using API.Services.Historicos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendariosController : ControllerBase
    {
        private readonly ICalendarioInterface _calendarioInterface;
        public CalendariosController(ICalendarioInterface calendarioInterface, HistoricoService historicoService)
        {
            _calendarioInterface = calendarioInterface;
        }

        [HttpGet("ListarCalendarios")]
        public async Task<ActionResult<ResponseModel<List<Calendario>>>> ListarCalendarios()
        {
            var calendarios = await _calendarioInterface.ListarCalendarios();
            return Ok(calendarios);
        }

        [HttpPost("CriarCalendario")]
        public async Task<ActionResult<ResponseModel<Calendario>>> CriarCalendario(CriarCalendarioDTO dados)
        {
            var calendario = await _calendarioInterface.CriarCalendario(dados);
            return Ok(calendario);
        }

        [HttpPost("AprovarCalendario/{idCalendario}")]
        public async Task<ActionResult<ResponseModel<Calendario>>> AprovarCalendario(int idCalendario)
        {
            var calendario = await _calendarioInterface.AprovarCalendario(idCalendario);
            return Ok(calendario);
        }

        [HttpPost("DesativarCalendario/{idCalendario}")]
        public async Task<ActionResult<ResponseModel<Calendario>>> DesativarCalendario(int idCalendario)
        {
            var calendario = await _calendarioInterface.DesativarCalendario(idCalendario);
            return Ok(calendario);
        }

        [HttpGet("CalendarioPorId/{idCalendario}")]
        public async Task<ActionResult<ResponseModel<Calendario>>> CalendarioPorId(int idCalendario)
        {
            var calendario = await _calendarioInterface.CalendarioPorId(idCalendario);
            return Ok(calendario);
        }

    }
}
