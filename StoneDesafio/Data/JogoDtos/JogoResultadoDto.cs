namespace StoneDesafio.Data.JogoDtos
{
    public class JogoResultadoDto
    {
        public int JogoId { get; set; }
        public int ClubeAId { get; set; }
        public int ClubeBId { get; set; }
        public string NomeClubeA { get; set; }
        public string NomeClubeB { get; set; }
        public int GolsClubeA { get; set; }
        public int GolsClubeB { get; set; }
        public string DataInicio { get; set; }
    }
}
