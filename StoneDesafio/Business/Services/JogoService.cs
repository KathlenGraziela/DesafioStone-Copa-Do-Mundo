using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Business.Services
{
    public class JogoService : IService<Jogo, JogoCriarDto, JogoEditarDto>
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Jogo> genericRepository;


        public JogoService(ModelConverter modelConverter, IRepository<Jogo> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<MensagemRota<Jogo>> CriarAsync(JogoCriarDto createDto)
        {
            var jogo = modelConverter.Convert<Jogo>(createDto);
            await genericRepository.AddAndSaveAsync(jogo);

            return new(MensagemResultado.Sucesso, "Jogo criado com sucesso!", jogo);
        }

        public async Task<MensagemRota<Jogo>> EditarAsync(JogoEditarDto editarDto)
        {
            var jogo = await genericRepository.FindAsync(editarDto.Id);
            if (jogo == null)
            {
                return new(MensagemResultado.Falha, "Jogo nao encontrado!");
            }

            modelConverter.ConvertInPlace(editarDto, jogo, checkNull: true);

            await genericRepository.UpdateAndSaveAsync(jogo);

            return new(MensagemResultado.Sucesso, "Jogo editado com sucesso!", jogo);
        }

        public async Task<MensagemRota<Jogo>> DeletarAsync(int id)
        {
            var jogo = await genericRepository.FindAsync(id);
            if (jogo == null)
            {
                return new(MensagemResultado.Falha, "Jogo nao encontrado!");
            }

            await genericRepository.RemoveAndSaveAsync(jogo);
            return new(MensagemResultado.Sucesso, "Jogo deletado com sucesso!");
        }
    }
}
