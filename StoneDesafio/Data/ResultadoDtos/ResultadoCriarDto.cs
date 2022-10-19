using StoneDesafio.Models;
using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.ResultadoDtos
{
    public class ResultadoCriarDto
    {
        [Required]
        public virtual Jogo Jogo { get; set; }
        [Required]
        public int GolsClubeA { get; set; }
        [Required]
        public int GolsClubeB { get; set; }
        [Required]
        public DateTime FimJogo { get; set; }

    }
}