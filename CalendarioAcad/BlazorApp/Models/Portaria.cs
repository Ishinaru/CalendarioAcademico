using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Portaria
    {
        public int Id_Portaria { get; set; }
        public string NumPortaria { get; set; }
        public int AnoPortaria { get; set; }
        public bool IsAtivo { get; set; } = true;
    }
}
