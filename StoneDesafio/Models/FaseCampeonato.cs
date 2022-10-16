using StoneDesafio.Enum;
using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Models
{
    public class FaseCampeonato
    {
        public Guid Id { get; set; }
        public EnumFasesCampeonato FasesCampeonato { get; set; }
        public List<Jogo> Jogos { get; set; }
    }
}
