namespace StoneDesafio.Models
{
    public class Resultado
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; }
        public int GolsClubeA { get; set; }
        public int GolsClubeB { get; set; }
        public DateTime FimJogo { get; set; }
    }
}
