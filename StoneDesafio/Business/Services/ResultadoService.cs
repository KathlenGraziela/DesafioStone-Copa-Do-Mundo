using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Business.Services
{
    public class ResultadoService : IService<Resultado, ResultadoCriarDto, ResultadoEditarDto>
    {
        private readonly ModelConverter modelConverter;
        private readonly IRepository<Resultado> genericRepository;


        public ResultadoService(ModelConverter modelConverter, IRepository<Resultado> genericRepository)
        {
            this.modelConverter = modelConverter;
            this.genericRepository = genericRepository;
        }

        public async Task<MensagemRota<Resultado>> CriarAsync(ResultadoCriarDto createDto)
        {
            //if (await genericRepository.FindFirstAsync(c => c.Nome == createDto.Nome) != null)
            //{
            //    return new(MensagemResultado.Falha, "Resultado com mesmo nome ja existe!");
            //}

            var resultado = modelConverter.Convert<Resultado>(createDto);

            await genericRepository.AddAndSaveAsync(resultado);

            return new(MensagemResultado.Sucesso, "Resultado criado com sucesso!", resultado);
        }

        public async Task<MensagemRota<Resultado>> EditarAsync(ResultadoEditarDto editarDto)
        {
            var resultado = await genericRepository.FindAsync(editarDto.Id);
            if (resultado == null)
            {
                return new(MensagemResultado.Falha, "Resultado não encontrado!");
            }

            modelConverter.ConvertInPlace(editarDto, resultado, checkNull: true);

            await genericRepository.UpdateAndSaveAsync(resultado);

            return new(MensagemResultado.Sucesso, "Resultado editado com sucesso!", resultado);
        }

        public async Task<MensagemRota<Resultado>> DeletarAsync(int id)
        {
            var resultado = await genericRepository.FindAsync(id);
            if (resultado == null)
            {
                return new(MensagemResultado.Falha, "Resultado nao encontrado!");
            }

            await genericRepository.RemoveAndSaveAsync(resultado);
            return new(MensagemResultado.Sucesso, "Resultado deletado com sucesso!");
        }
    }
}
