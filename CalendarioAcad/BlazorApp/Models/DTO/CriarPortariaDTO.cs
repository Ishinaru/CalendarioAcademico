namespace BlazorApp.Models.DTO
{
    public class CriarPortariaDTO
    {
        public string NumPortaria { get; set; }

        public int AnoPortaria { get; set; }

        public int IdUsuario { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        public string? Observacao { get; set; }
    }
}