using StoneDesafio.Models;
using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.ResultadoDtos
{
    public class ResultadoEditarDto
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; }
        public int GolsClubeA { get; set; }
        public int GolsClubeB { get; set; }
        public DateTime FimJogo { get; set; }

    }
}