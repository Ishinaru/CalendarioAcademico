using API.Model;
using API.Services.Historicos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly IHistoricoInterface _historicoInterface;
        public HistoricoController(IHistoricoInterface historicoInterface)
        {
            _historicoInterface = historicoInterface;
        }

        [HttpGet("ListarHistoricos")]
        public async Task<ActionResult<ResponseModel<List<Historico>>>> ListarHistoricos()
        {
            var historicos = await _historicoInterface.ListarHistoricos();
            return Ok(historicos);
        }

    }
}
