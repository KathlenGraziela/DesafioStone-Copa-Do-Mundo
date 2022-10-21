using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.GrupoDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    [Authorize]
    public class GrupoController : GenericController<Grupo, GrupoCriarDto, GrupoEditarDto>
    {
        private readonly IRepository<Clube> clubeRepository;

        public GrupoController(IRepository<Grupo> repository, IService<Grupo, GrupoCriarDto, GrupoEditarDto> service, IRepository<Clube> clubeRepository) : base(repository, service)
        {
            this.clubeRepository = clubeRepository;
        }
        public override async Task<IActionResult> Index(MensagemRota<Grupo> msg = null)
        {
            var grupos = await repository.GetSet()
                .Include(g => g.Clubes)
                .ToListAsync();
            return View(grupos);
        }
    }
}
