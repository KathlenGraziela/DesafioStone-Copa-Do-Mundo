using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities.Encoders;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;
using System.Text;

namespace StoneDesafio.Business.Services
{
    public class ClubeService : IService<Clube, ClubeCriarDto, ClubeEditarDto>
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Clube> genericRepository;


        public ClubeService(ModelConverter modelConverter, IRepository<Clube> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<MensagemRota<Clube>> CriarAsync(ClubeCriarDto createDto)
        {
            if (await genericRepository.FindFirstAsync(c => c.Nome == createDto.Nome) != null)
            {
                return new(MensagemResultado.Falha, "Clube com mesmo nome ja existe!");
            }

            var clube = modelConverter.Convert<Clube>(createDto);

            await genericRepository.AddAndSaveAsync(clube);

            return new(MensagemResultado.Sucesso, "Clube criado com sucesso!", clube);
        }

        public async Task<MensagemRota<Clube>> EditarAsync(ClubeEditarDto editarDto)
        {
            var clube = await genericRepository.FindAsync(editarDto.Id);
            if (clube == null)
            {
                return new(MensagemResultado.Falha, "Clube nao encontrado!");
            }

            modelConverter.ConvertInPlace(editarDto, clube, checkNull: true);

            await genericRepository.UpdateAndSaveAsync(clube);

            return new(MensagemResultado.Sucesso, "Clube editado com sucesso!", clube);
        }

        public async Task<MensagemRota<Clube>> DeletarAsync(int id)
        {
            var clube = await genericRepository.FindAsync(id);
            if (clube == null)
            {
                return new(MensagemResultado.Falha, "Clube nao encontrado!");
            }

            await genericRepository.RemoveAndSaveAsync(clube);
            return new(MensagemResultado.Sucesso, "Clube deletado com sucesso!");
        }
    }
}
