using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class Historico
    {
        [Key]
        public int IdHistorico { get; set; }
        public Status Status { get; set; }
        public string Descricao { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataMudanca { get; set; }


        public int ?IdCalendario { get; set; }
        public Calendario ?Calendario { get; set; }

        public int ?IdEvento { get; set; }
        public Evento? Evento { get; set; }

        public int ?IdPortaria { get; set; }
        public Portaria ?Portaria { get; set; }
    }
}
