using GenericRepositoryBuilder;
using StoneDesafio.Models;

namespace StoneDesafio.Business.Repositorys
{
    public interface IRepository<T> : IGenericRepository<T> where T : class
    {
    }
}
