using GenericRepositoryBuilder;
using StoneDesafio.Businesss;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models.Utils;
using StoneDesafio.Models;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;

namespace StoneDesafio.Business.Services
{
    public interface IService<TModel, TCriarDto, TEditarDto> where TModel : class
    {
        public Task<MensagemRota<TModel>> CriarAsync(TCriarDto criarDto);

        public Task<MensagemRota<TModel>> EditarAsync(TEditarDto editarDto);

        public  Task<MensagemRota<TModel>> DeletarAsync(int id);
    }
}