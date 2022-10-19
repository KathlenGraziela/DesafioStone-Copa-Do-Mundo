using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneDesafio.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public Guid ClubeAId { get; set; }
        [Description("Clube A")]
        public virtual Clube ClubeA { get; set; }
        public Guid ClubeBId { get; set; }
        [Description("Clube B")]
        public virtual Clube ClubeB { get; set; }

        public DateTime InicioJogo { get; set; }
    }
}
