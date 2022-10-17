using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Models
{
    public class Login
    {
        [MaxLength(150)]
        [Required(ErrorMessage = "Digite o email do usuário")]
        public string Email { get; set; }

        [MaxLength(300)]
        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }
    }
}
