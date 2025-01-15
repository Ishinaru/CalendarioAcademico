using API.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTO.Portarias
{
    public class CriarPortariaDTO
    {
        public int NumPortaria { get; set; }

        public int AnoPortaria { get; set; }

        public int IdUsuario { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}
