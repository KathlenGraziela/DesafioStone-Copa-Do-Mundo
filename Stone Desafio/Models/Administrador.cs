using System.ComponentModel.DataAnnotations;

namespace Stone_Desafio.Models
{
    public class Administrador
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string Senha { get; set; }
    }
}
