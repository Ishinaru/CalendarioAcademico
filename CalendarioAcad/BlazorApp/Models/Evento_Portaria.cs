namespace BlazorApp.Models
{
    public class Evento_Portaria
    {
        public int EventoID { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFinal { get; set; }
        public string Observacao { get; set; }
    }
}
