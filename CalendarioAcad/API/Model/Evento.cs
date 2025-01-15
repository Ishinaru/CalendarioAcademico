using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
    public class Evento
    {
        [Key]
        public int IdEvento { get; set; }

        [Required(ErrorMessage ="Data inicial é obrigatória")]
        public DateOnly DataInicio { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        
        [Required(ErrorMessage = "Data final é obrigatória")]
        public DateOnly DataFinal { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string? Descricao { get; set; }
        
        [Required(ErrorMessage = "Campo Uesc Funciona é obrigatório")]
        public bool UescFunciona { get; set; }
        
        [Required(ErrorMessage = "Campo Importante é obrigatório")]
        public bool Importante { get; set; }
        
        [Required(ErrorMessage = "Campo TipoFeriado é obrigatório")]
        public TipoFeriado TipoFeriado { get; set; }

        public int IdUsuario { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        public bool IsAtivo { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Evento_Portaria> Eventos_Portarias { get; set; }

        [JsonIgnore]
        public virtual ICollection<Historico> Historicos { get; set; }

        public int IdCalendario { get; set; }
        public virtual Calendario Calendario { get; set; }

    }
}
