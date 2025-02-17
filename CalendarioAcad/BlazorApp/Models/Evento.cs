namespace BlazorApp.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public DateOnly DataInicio { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateOnly DataFinal { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public string? Descricao { get; set; }
        public bool UescFunciona { get; set; }
        public bool Importante { get; set; }
        public TipoFeriado TipoFeriado { get; set; }
        public int IdCalendario { get; set; }
    }

    public static class EnumExtensionsBuild
    {
        public static string ConverteEnum(this Enum valor)
        {
            return valor.ToString().Replace("_", " ");
        }
    }
}
