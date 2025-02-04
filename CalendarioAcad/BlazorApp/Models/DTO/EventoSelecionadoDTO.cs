namespace BlazorApp.Models.DTO
{
    public class EventoSelecionadoDTO
    {
        public int IdEvento { get; set; }
        public bool Selecionado { get; set; }
        public DateOnly NovaDataInicio { get; set; }
        public DateOnly NovaDataFinal { get; set; }
        public string Observacao { get; set; }
    }
}
