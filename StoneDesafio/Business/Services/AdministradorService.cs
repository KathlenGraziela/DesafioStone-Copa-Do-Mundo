using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using System.Diagnostics.Metrics;
using System.Text;

namespace StoneDesafio.Business.Services
{
    public class AdministradorService : IService<Administrador, AdministradorCriarDto, AdministradorEditarDto>
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Administrador> genericRepository;

        public AdministradorService(ModelConverter modelConverter, IRepository<Administrador> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<MensagemRota<Administrador>> CriarAsync(AdministradorCriarDto createDto)
        {
            if (await genericRepository.FindFirstAsync(a => a.Email == createDto.Email) != null)
            {
                return new(MensagemResultado.Falha, $"Administador com email {createDto.Email} já existe");
            }

            var senhaCript = CriptografiaService.Criptografar(createDto.Senha);
            var administrador = modelConverter.Convert<Administrador>(createDto);
            administrador.Senha = senhaCript;

            await genericRepository.AddAndSaveAsync(administrador);

            return new(MensagemResultado.Sucesso, null, administrador);
        }

        public async Task<MensagemRota<Administrador>> EditarAsync(AdministradorEditarDto editarDto)
        {
            var administrador = await genericRepository.FindAsync(editarDto.Id);
            if(administrador == null)
            {
                return new(MensagemResultado.Falha, $"Administador com id {editarDto.Id} não foi encontrado");
            }

            modelConverter.ConvertInPlace(editarDto, administrador, checkNull: true);

            if(!string.IsNullOrEmpty(editarDto.Senha))
            {
                var senhaCript = CriptografiaService.Criptografar(editarDto.Senha);
                administrador.Senha = senhaCript;
            }

            await genericRepository.UpdateAndSaveAsync(administrador);

            return new(MensagemResultado.Sucesso, null, administrador);
        }

        public async Task<MensagemRota<Administrador>> DeletarAsync(int id)
        {
            var administrador = await genericRepository.FindAsync(id);
            if(administrador == null)
            {
                return new(MensagemResultado.Falha, $"Administador com id {id} não foi encontrado");
            }

            await genericRepository.RemoveAndSaveAsync(administrador);

            return new(MensagemResultado.Sucesso, null);
        }
    }
}
