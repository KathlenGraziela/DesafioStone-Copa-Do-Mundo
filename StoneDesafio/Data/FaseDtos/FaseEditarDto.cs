using StoneDesafio.Enum;
using StoneDesafio.Models;

namespace StoneDesafio.Data.FaseDtos
{
    public class FaseEditarDto
    {
        public int Id { get; set; }
        public virtual List<int> Jogos { get; set; }
    }
}
