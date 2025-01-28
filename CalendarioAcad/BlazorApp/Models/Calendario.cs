namespace BlazorApp.Models
{
    public class Calendario
    {
        public int IdCalendario { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public Status Status { get; set; } = Status.AguardandoAprovacao;
        public string NumeroResolucao { get; set; }
        public string Observacao { get; set; }
        public ICollection<Evento>? Eventos { get; set; }


    }
}
