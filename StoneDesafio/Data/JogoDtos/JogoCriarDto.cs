using StoneDesafio.Models;

namespace StoneDesafio.Data.JogoDtos
{
    public class JogoCriarDto
    {

        public int ClubeAId { get; set; }
        public int ClubeBId { get; set; }
        public int FaseId { get; set; }
        public DateTime InicioJogo { get; set; }

    }
}