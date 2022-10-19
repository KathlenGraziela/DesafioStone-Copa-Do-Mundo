using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Models
{
    public class Clube
    {
<<<<<<< HEAD
        public int Id { get; set; }

        [MaxLength(100)]
=======
        public Guid Id { get; set; }
>>>>>>> 02b49d4b40b79d4546ba1320617cf79b40382f6d
        public string Nome { get; set; }

        [MaxLength(300)]
        public string Descricao { get; set; }

        [MaxLength(300)]
        public string UrlFoto { get; set; }
    }
}
