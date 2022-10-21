﻿namespace StoneDesafio.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Clube> Clubes { get; set; }
    }
}
