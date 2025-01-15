using API.DTO.EventoPortarias;
using API.DTO.Portarias;
using API.Model;
using API.Services.Portarias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortariaController : ControllerBase
    {
        private readonly IPortariaInterface _portariaInterface;
        public PortariaController(IPortariaInterface portariaInterface)
        {
            _portariaInterface = portariaInterface;
        }

        [HttpPost("CriarPortaria")]
        public async Task <ActionResult<ResponseModel<Portaria>>> CriarPortaria(CriarPortariaEventosDTO dados)
        {
            CriarPortariaDTO portariaDTO = dados.PortariaDTO;
            var eventoPortariasDTO = dados.EventoPortariasDTO;
            var portaria = await _portariaInterface.CriarPortaria(portariaDTO, eventoPortariasDTO);
            return Ok(portaria);
        }

        [HttpGet("ListarPortarias")]
        public  async Task <ActionResult<ResponseModel<Portaria>>> ListarPortarias()
        {
            var portarias = await _portariaInterface.ListarPortarias();
            return Ok(portarias);
        }

        [HttpPost("DesativarPortaria/{idPortaria}")]
        public async Task<ActionResult<ResponseModel<Portaria>>> DesativarPortaria(int idPortaria)
        {
            var portaria = await _portariaInterface.DesativarPortaria(idPortaria);
            return Ok(portaria);
        }
    }
}
