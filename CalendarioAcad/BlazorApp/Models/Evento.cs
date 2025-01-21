using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public DateOnly DataInicio { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        public DateOnly DataFinal { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        public string? Descricao { get; set; }
        public bool UescFunciona { get; set; }
        public bool Importante { get; set; }
        public TipoFeriado TipoFeriado { get; set; }

    }
}
