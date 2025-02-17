namespace API.DTO.Portarias
{
    public class CriarPortariaDTO
    {
        public string NumPortaria { get; set; }

        public int AnoPortaria { get; set; }

        public int IdUsuario { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string? Observacao { get; set; }
    }
}
