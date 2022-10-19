using StoneDesafio.Enum;

namespace StoneDesafio.Data
{
    public class FaseEditarDto
    {
        public FasesCampeonato FasesCampeonato { get; set; }
        public List<Guid> Jogos { get; set; }
    }
}
