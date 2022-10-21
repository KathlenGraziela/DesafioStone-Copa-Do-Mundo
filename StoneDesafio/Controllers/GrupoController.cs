using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.GrupoDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    [Authorize]
    public class GrupoController : GenericController<Grupo, GrupoCriarDto, GrupoEditarDto>
    {
        public GrupoController(IRepository<Grupo> repository, IService<Grupo, GrupoCriarDto, GrupoEditarDto> service) : base(repository, service)
        {

        }
    }
}
