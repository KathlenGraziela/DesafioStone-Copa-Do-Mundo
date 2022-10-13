using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Businesss;
using StoneDesafio.Data;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using System.Text;

namespace StoneDesafio.Business.Services
{
    public class AdministradorService
    {
        private readonly AppDbContext dbContext;
        private readonly ModelConverter modelConverter;
        private readonly AdministradorRepository administradorRepository;
        private static readonly string salt = 
            Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("saltDB") ?? "salter"));

        public AdministradorService(AppDbContext dbContext, AdministradorRepository administradorRepository, ModelConverter modelConverter)
        {
            this.dbContext = dbContext;
            this.administradorRepository = administradorRepository;
            this.modelConverter = modelConverter;
        }

        public async Task<Administrador> CriarAsync(AdministradorCreateDto createDto)
        {
            var senhaCript = CriptografiaService.Criptografar(createDto.Senha);
            var administrador = new Administrador
            {
                Id = Guid.NewGuid(),
                Nome = createDto.Nome,
                Email = createDto.Email,
                Senha = senhaCript
            };

            await administradorRepository.CriarAsync(administrador);

            return administrador;
        }

        public async Task<Administrador> EditarAsync(Guid id, AdministradorEditDto editDto)
        {
            var administrador = await administradorRepository.SelectByIdRequiredAsync(id);

            modelConverter.ConvertInPlace(editDto, administrador, checkNull: true);

            if(!string.IsNullOrEmpty(editDto.Senha))
            {
                var senhaCript = CriptografiaService.Criptografar(editDto.Senha);
                administrador.Senha = senhaCript;
            }

            await administradorRepository.EditarAsync(administrador);

            return administrador;
        }
    }
}
