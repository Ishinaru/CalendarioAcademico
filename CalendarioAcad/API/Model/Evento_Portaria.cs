namespace API.Model
{
    public class Evento_Portaria
    {
        public int Id { get; set; }

        public int IdEvento { get; set; }
        public virtual Evento Evento { get; set; }

        public int IdPortaria { get; set; }
        public virtual Portaria Portaria { get; set; }

        public DateOnly DataInicio { get; set; }

        public DateOnly DataFinal { get; set; }

        public string? Observacao { get; set; }
    }
}
