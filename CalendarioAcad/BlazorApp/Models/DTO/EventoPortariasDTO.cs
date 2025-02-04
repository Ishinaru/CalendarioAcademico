namespace BlazorApp.Models.DTO
{
    public class EventoPortariasDTO
    {
        public int EventoID { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFinal { get; set; }
        public string Observacao { get; set; }
    }
}