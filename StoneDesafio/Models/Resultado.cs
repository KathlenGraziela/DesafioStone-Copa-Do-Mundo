namespace StoneDesafio.Models
{
    public class Resultado
    {
        public Guid Id { get; set; }
        public Guid JogoId { get; set; }
        public virtual Jogo Jogo { get; set; }
        public int GolsClubeA { get; set; }
        public int GolsClubeB { get; set; }
        public DateTime FimJogo { get; set; }
    }
}
