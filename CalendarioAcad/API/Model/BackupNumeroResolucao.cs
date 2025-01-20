using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class BackupNumeroResolucao
    {
        [Key]
        public int Id { get; set; }
        public string NumResolucao { get; set; }
    }
}
