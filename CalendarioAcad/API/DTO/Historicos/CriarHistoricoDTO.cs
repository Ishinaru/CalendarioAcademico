using API.Model;

namespace API.DTO.Historicos
{
    public class CriarHistoricoDTO
    {
        public Status Status { get; set; }
        public string Descricao { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataMudanca { get; set; } = DateTime.Now;

        public int? IdCalendario { get; set; }
        public Calendario? Calendario { get; set; }

        public int? IdEvento { get; set; }
        public Evento? Evento { get; set; }

        public int? IdPortaria { get; set; }
        public Portaria? Portaria { get; set; }
    }
}
