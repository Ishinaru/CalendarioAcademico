using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
    public class Portaria
    {
        [Key]
        public int Id_Portaria { get; set; }
        [Required(ErrorMessage = "Número da portaria necessário")]
        public string NumPortaria { get; set; }
        [Required(ErrorMessage = "Ano da portaria necessário")]
        public int AnoPortaria { get; set; }
        public int IdUsuario { get; set; }
        public bool IsAtivo { get; set; } = true;

        public DateTime DataAtualizacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Evento_Portaria> Eventos_Portarias { get; set; }

        [JsonIgnore]
        public virtual ICollection<Historico> Historicos { get; set; }
    }
}
