using System.ComponentModel.DataAnnotations;

namespace Stone_Desafio.Models
{
    public class FaseCampeonato
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public List<Jogo> Jogos { get; set; }
    }
}
