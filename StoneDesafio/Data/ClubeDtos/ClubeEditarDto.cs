using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data.ClubeDtos
{
    public class ClubeEditarDto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }


        [MaxLength(100)]
        public string Descricao { get; set; }

        [MaxLength(300)]
        public string UrlFoto { get; set; }

    }
}