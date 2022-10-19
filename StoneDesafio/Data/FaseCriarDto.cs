using StoneDesafio.Enum;
using StoneDesafio.Models;

namespace StoneDesafio.Data
{
    public class FaseCriarDto
    {
        public FasesCampeonato FasesCampeonato { get; set; }
        public List<int> Jogos { get; set; }
    }

}
