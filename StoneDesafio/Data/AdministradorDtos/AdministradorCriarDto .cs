using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.AdministradorDtos
{
    public class AdministradorCriarDto
    {
        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }

        [MaxLength(150)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        [Required]
        public string Senha { get; set; }
    }
}
