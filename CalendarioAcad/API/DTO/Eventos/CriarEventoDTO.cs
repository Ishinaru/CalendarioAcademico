using API.Model;

namespace API.DTO.Eventos
{
    public class CriarEventoDTO
    {
        public DateOnly DataInicio { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        public DateOnly DataFinal { get; set; } = new DateOnly(DateTime.Now.Year, 1, 1);
        public string? Descricao { get; set; }
        public bool UescFunciona { get; set; }
        public bool Importante { get; set; }
        public TipoFeriado TipoFeriado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
