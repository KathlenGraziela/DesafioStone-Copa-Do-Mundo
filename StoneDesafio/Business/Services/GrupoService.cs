using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Data.GrupoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using System.Text;
using System.Text.RegularExpressions;

namespace StoneDesafio.Business.Services
{
    public class GrupoService : IService<Grupo, GrupoCriarDto, GrupoEditarDto>
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Grupo> genericRepository;


        public GrupoService(ModelConverter modelConverter, IRepository<Grupo> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<MensagemRota<Grupo>> CriarAsync(GrupoCriarDto createDto)
        {
            if (await genericRepository.FindFirstAsync(g => g.Nome == createDto.Nome) != null)
            {
                return new(MensagemResultado.Falha, "Grupo com mesmo nome ja existe!");
            }

            var grupo = modelConverter.Convert<Grupo>(createDto);

            await genericRepository.AddAndSaveAsync(grupo);

            return new(MensagemResultado.Sucesso, "Grupo criado com sucesso!", grupo);
        }

        public async Task<MensagemRota<Grupo>> EditarAsync(GrupoEditarDto editarDto)
        {
            var grupo = await genericRepository.FindAsync(editarDto.Id);
            if (grupo == null)
            {
                return new(MensagemResultado.Falha, "Grupo nao encontrado!");
            }

            modelConverter.ConvertInPlace(editarDto, grupo, checkNull: true);

            await genericRepository.UpdateAndSaveAsync(grupo);

            return new(MensagemResultado.Sucesso, "Grupo editado com sucesso!", grupo);
        }

        public async Task<MensagemRota<Grupo>> DeletarAsync(int id)
        {
            var grupo = await genericRepository.FindAsync(id);
            if (grupo == null)
            {
                return new(MensagemResultado.Falha, "Grupo não encontrado!");
            }

            await genericRepository.RemoveAndSaveAsync(grupo);
            return new(MensagemResultado.Sucesso, "Grupo deletado com sucesso!");
        }
    }
}
