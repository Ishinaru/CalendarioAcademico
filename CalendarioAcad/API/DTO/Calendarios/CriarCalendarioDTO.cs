namespace API.DTO.Calendarios
{
    public class CriarCalendarioDTO
    {
        public int Ano { get; set; } = DateTime.Now.Year;
        public int IdUsuario { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string? NumeroResolucao { get; set; }
        public string? Observacao { get; set; }
    }
}
