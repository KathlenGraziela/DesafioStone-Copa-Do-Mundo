using System.ComponentModel.DataAnnotations;

namespace Stone_Desafio.Models
{
    public class Clube
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        string Nome { get; set; }
        
        [MaxLength(500)]
        string Descricao { get; set; }

        [MaxLength(300)]
        string UrlFoto { get; set; }
    }
}
