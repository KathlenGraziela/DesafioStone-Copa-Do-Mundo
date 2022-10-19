using StoneDesafio.Models;

namespace StoneDesafio.Data.JogoDtos
{
    public class JogoEditarDto
    {
        public int Id { get; set; }

        public Clube ClubeA { get; set; }

        public Clube ClubeB { get; set; }

        public int GolsClubeA { get; set; }

        public int GolsClubeB { get; set; }

        public DateTime InicioJogo { get; set; }

        public DateTime FimJogo { get; set; }
    }
}