using System.ComponentModel.DataAnnotations;

namespace Stone_Desafio.Data
{
    public class AdministradorEditDto
    {
        [MaxLength(100)]
        public string? Nome { get; set; }

        [MaxLength(100)]
        public string? Senha { get; set; }
    }
}
