using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.AdministradorDtos
{
    public class AdministradorReadDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
