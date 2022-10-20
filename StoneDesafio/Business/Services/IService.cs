using StoneDesafio.Models;

namespace StoneDesafio.Business.Services
{
    public interface IService<TModel, TCriarDto, TEditarDto> where TModel : class
    {
        public Task<MensagemRota<TModel>> CriarAsync(TCriarDto criarDto);

        public Task<MensagemRota<TModel>> EditarAsync(TEditarDto editarDto);

        public  Task<MensagemRota<TModel>> DeletarAsync(int id);
    }
}