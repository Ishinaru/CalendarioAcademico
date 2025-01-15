using API.DTO.EventoPortarias;

namespace API.DTO.Portarias
{
    public class CriarPortariaEventosDTO
    {
        public CriarPortariaDTO PortariaDTO { get; set; }
        public List<EventoPortariasDTO> EventoPortariasDTO { get; set; }
    }
}
