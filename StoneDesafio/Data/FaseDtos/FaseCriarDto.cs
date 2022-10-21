using StoneDesafio.Enum;
using StoneDesafio.Models;

namespace StoneDesafio.Data.FaseDtos
{
    public class FaseCriarDto
    {
        public FasesCampeonato FaseAtualCampeonato { get; set; }
        public List<int> Jogos { get; set; }
    }

}
