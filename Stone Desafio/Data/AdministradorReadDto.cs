using System.ComponentModel.DataAnnotations;

namespace Stone_Desafio.Data
{
    public class AdministradorReadDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
