﻿using System.ComponentModel.DataAnnotations;

namespace StoneDesafio.Data
{
    public class AdministradorCreateDto
    {
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Senha { get; set; }
    }
}
