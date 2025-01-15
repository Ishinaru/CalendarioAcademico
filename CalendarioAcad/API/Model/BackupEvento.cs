using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class BackupEvento
    {
        [Key]
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFinal { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
