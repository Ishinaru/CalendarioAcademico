using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
    public class Calendario
    {
        [Key]
        public int IdCalendario { get; set; }

        [Required(ErrorMessage = "Campo Ano é obrigatório")]
        public int Ano { get; set; } = DateTime.Now.Year;

        public int IdUsuario { get; set; }
        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "Campo Status é obrigatório")]
        public Status Status { get; set; } = Status.AguardandoAprovacao;

        public string? NumeroResolucao { get; set; }

        public string? Observacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Evento>? Eventos { get; set; }

        [JsonIgnore]
        public virtual ICollection<Historico>? Historicos { get; set; }


    }
}
