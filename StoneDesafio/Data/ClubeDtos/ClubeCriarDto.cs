using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.ClubeDtos
{
    public class ClubeCriarDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [MaxLength(300)]
        public string UrlFoto { get; set; }
    }
}