using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Models
{
    public class Administrador
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string Senha { get; set; }
    }
}
