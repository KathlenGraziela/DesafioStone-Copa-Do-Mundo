using StoneDesafio.Enum;
using StoneDesafio.Models;

namespace StoneDesafio.Data
{
    public class FaseCriarDto
    {
        public EnumFasesCampeonato FasesCampeonato { get; set; }
        public List<Guid> Jogos { get; set; }
    }

}
