namespace BlazorApp.Models.DTO.Calendario
{
    public class CriarCalendarioDTO
    {
        public int Ano { get; set; } = DateTime.Now.Year;
        public string? Observacao { get; set; }
    }
}
