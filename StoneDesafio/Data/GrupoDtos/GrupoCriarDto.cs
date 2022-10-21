using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.GrupoDtos
{
    public class GrupoCriarDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

    }
}