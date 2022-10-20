using StoneDesafio.Models;

namespace StoneDesafio.Data.JogoDtos
{
    public class JogoEditarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ClubeAId { get; set; }

        public int ClubeBId { get; set; }

        public int GolsClubeA { get; set; }

        public int GolsClubeB { get; set; }

        public int FaseId { get; set; }

        public DateTime InicioJogo { get; set; }

    }
}