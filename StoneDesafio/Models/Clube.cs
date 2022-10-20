using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Models
{
    public class Clube
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [MaxLength(300)]
        public string Descricao { get; set; }
        [MaxLength(300)]
        public string UrlFoto { get; set; }
        public int GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
