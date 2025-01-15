using System.ComponentModel;

namespace API.Model
{
    public enum TipoFeriado
    {
        [Description("Municipal")]
        Municipal = 1,
        [Description("Estadual")]
        Estadual = 2,
        [Description("Nacional")]
        Nacional = 3,
        [Description("Não Feriado")]
        Não_Feriado = 0
    }
}
