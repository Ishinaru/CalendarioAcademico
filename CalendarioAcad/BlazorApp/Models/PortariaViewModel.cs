using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class PortariaViewModel
    {
        [Required(ErrorMessage = "O número da portaria é obrigatório.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "O número da portaria deve ter 3 dígitos numéricos.")]
        public string NumPortaria { get; set; } = string.Empty;

        [Required(ErrorMessage = "A observação é obrigatória.")]
        public string Observacao { get; set; } = string.Empty;
    }
}
