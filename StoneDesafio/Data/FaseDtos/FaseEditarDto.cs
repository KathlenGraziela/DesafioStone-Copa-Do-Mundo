using StoneDesafio.Enum;

namespace StoneDesafio.Data.FaseDtos
{
    public class FaseEditarDto
    {
        public FasesCampeonato FasesCampeonato { get; set; }
        public List<Guid> Jogos { get; set; }
    }
}
