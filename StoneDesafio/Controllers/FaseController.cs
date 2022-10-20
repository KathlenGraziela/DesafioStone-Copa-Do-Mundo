using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.FaseDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Controllers
{
    public class FaseController : GenericController<FaseCampeonato, FaseCriarDto, FaseEditarDto>
    {
        private readonly IRepository<Jogo> jogoRepository;
        public FaseController(IRepository<FaseCampeonato> repository, IService<FaseCampeonato, FaseCriarDto, FaseEditarDto> service, IRepository<Jogo> jogoRepository) : base(repository, service)
        {
            this.jogoRepository = jogoRepository;
        }
        public override async Task<IActionResult> Index(MensagemRota<FaseCampeonato> msg = null)
        {
            ViewBag.msg = msg;
            var fases = await repository.GetSet()
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeA)
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeB).ToListAsync();

            return View(fases);
        }

        public override async Task<IActionResult> Criar()
        {
            var jogos = await jogoRepository.SelectAllAsync();
            ViewData["ListaJogos"] = jogos;
            return View();
        }

        public override async Task<IActionResult> Editar(int id)
        {
            var fase = await repository.GetSet()
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeA)
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeB).FirstOrDefaultAsync(f => f.Id == id);

            var jogos = await jogoRepository.SelectAllAsync();
            ViewData["ListaJogos"] = jogos;

            return View(fase);
        }
    }
}
