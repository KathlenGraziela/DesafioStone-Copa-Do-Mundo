using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.AdministradorDtos
{
    public class AdministradorLoginDto
    {
        [MaxLength(150)]
        [Required(ErrorMessage = "Digite o email do usuário")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }
    }
}
