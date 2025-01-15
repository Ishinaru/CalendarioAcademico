using API.Model;
using System.ComponentModel.DataAnnotations;

namespace API.DTO.Eventos
{
    public class EditarEventoDTO
    {
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFinal { get; set; }
        public string Descricao { get; set; }
        public bool UescFunciona { get; set; }
        public bool Importante { get; set; }
        public TipoFeriado TipoFeriado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
