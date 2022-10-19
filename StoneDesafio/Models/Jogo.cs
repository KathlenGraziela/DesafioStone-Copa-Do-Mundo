using System.ComponentModel.DataAnnotations.Schema;

namespace StoneDesafio.Models
{
    public class Jogo
    {
        public Guid Id { get; set; }
        public int ClubeAId { get; set; }
        public virtual Clube ClubeA { get; set; }
        public int ClubeBId { get; set; }
        public virtual Clube ClubeB { get; set; }
        public DateTime InicioJogo { get; set; }
    }
}
