using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Businesss;
using StoneDesafio.Data.AdministradorDtos;
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
        private readonly IRepository<Administrador> genericRepository;


        public AdministradorService(AppDbContext dbContext, ModelConverter modelConverter, IRepository<Administrador> genericRepository)
        {
            this.dbContext = dbContext;
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<Administrador> CriarAsync(AdministradorCriarDto createDto)
        {
            if (await genericRepository.FindFirstAsync(a => a.Email == createDto.Email) != null)
            {
                throw new ApiException($"Administador com email {createDto.Email} já existe");
            }

            var senhaCript = CriptografiaService.Criptografar(createDto.Senha);
            var administrador = modelConverter.Convert<Administrador>(createDto);
            administrador.Senha = senhaCript;

            await genericRepository.AddAndSaveAsync(administrador);

            return administrador;
        }

        public async Task<Administrador> EditarAsync(int id, AdministradorEditarDto editDto)
        {
            var administrador = await genericRepository.FindAsync(id)  ??
                throw new ApiException($"Administador com id {id} não foi encontrado"); 

            modelConverter.ConvertInPlace(editDto, administrador, checkNull: true);

            if(!string.IsNullOrEmpty(editDto.Senha))
            {
                var senhaCript = CriptografiaService.Criptografar(editDto.Senha);
                administrador.Senha = senhaCript;
            }

            await genericRepository.UpdateAndSaveAsync(administrador);

            return administrador;
        }

        public async Task DeletarAsync(int id)
        {
            var administrador = await genericRepository.FindAsync(id) ??
                    throw new ApiException($"Administador com id {id} não foi encontrado");
            
            await genericRepository.RemoveAndSaveAsync(administrador);
        }
    }
}
