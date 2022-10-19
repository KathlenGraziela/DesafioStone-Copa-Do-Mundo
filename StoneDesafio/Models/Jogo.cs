using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneDesafio.Models
{
    public class Jogo
    {
<<<<<<< HEAD
        public int Id { get; set; }

        public Clube ClubeA { get; set; }

        public Clube ClubeB { get; set; }

        public int GolsClubeA { get; set; }

        public int GolsClubeB { get; set; }

=======
        public Guid Id { get; set; }
        public Guid ClubeAId { get; set; }
        [Description("Clube A")]
        public virtual Clube ClubeA { get; set; }
        public Guid ClubeBId { get; set; }
        [Description("Clube B")]
        public virtual Clube ClubeB { get; set; }
>>>>>>> 02b49d4b40b79d4546ba1320617cf79b40382f6d
        public DateTime InicioJogo { get; set; }
    }
}
