using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.GrupoDtos
{
    public class GrupoEditarDto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
    }
}