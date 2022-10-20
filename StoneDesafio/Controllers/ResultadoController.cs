using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Controllers.Teste;
using StoneDesafio.Data.ResultadoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ResultadoController : GenericController<Resultado, ResultadoCriarDto, ResultadoEditarDto>
    {
        public ResultadoController(IRepository<Resultado> repository, IService<Resultado, ResultadoCriarDto, ResultadoEditarDto> service) : base(repository, service)
        {
        }
    }
}
