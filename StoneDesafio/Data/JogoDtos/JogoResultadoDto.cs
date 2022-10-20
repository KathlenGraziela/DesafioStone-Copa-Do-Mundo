using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.ResultadoDtos;

namespace StoneDesafio.Data.JogoDtos
{
    public class JogoResultadoDto
    {
        public int Id { get; set; }
        public string DataInicio { get; set; }
        public ClubeDetalheDto ClubeA  { get; set; }
        public ClubeDetalheDto ClubeB  { get; set; }
        public ResultadoDetalheDto Resultado { get; set; }
    }
}
