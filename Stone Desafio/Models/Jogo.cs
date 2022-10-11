using System.ComponentModel.DataAnnotations.Schema;

namespace Stone_Desafio.Models
{
    public class Jogo
    {
        public Guid Id { get; set; }

        public Clube ClubeA { get; set; }

        public Clube ClubeB { get; set; }

        public int GolsClubeA { get; set; }

        public int GolsClubeB { get; set; }

        public DateTime InicioJogo { get; set; }

        [NotMapped]
        public TimeSpan Tempo_atual 
        { 
            get {
                return DateTime.Now - InicioJogo;
            }
        }
    }
}
