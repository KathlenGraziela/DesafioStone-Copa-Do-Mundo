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
    public class FaseController : Controller
    {
        private readonly IRepository<Jogo> jogoRepository;
        private readonly IRepository<FaseCampeonato> repository;
        private readonly FaseService service;
        public FaseController(IRepository<FaseCampeonato> repository, IRepository<Jogo> jogoRepository, FaseService service)
        {
            this.jogoRepository = jogoRepository;
            this.repository = repository;
            this.service = service;
        }

        public async Task<IActionResult> Index(MensagemRota<FaseCampeonato> msg = null)
        {
            if (msg.Mensagem != null)
            {
                ViewData["Mensagem"] = msg;
            }

            var fases = await repository.GetSet()
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeA)
                .Include(f => f.Jogos)
                .ThenInclude(j => j.ClubeB).ToListAsync();

            return View(fases);
        }
        
        [Route("Criar")]
        public async Task<IActionResult> Criar()
        {
            var jogos = await jogoRepository.SelectAllAsync();
            ViewData["ListaJogos"] = jogos;
            return View();
        }

        [HttpPost, Route("Criar")]
        virtual public async Task<IActionResult> CriarPost(FaseCriarDto criarDto)
        {
            if (!ModelState.IsValid)
            {
                return View(criarDto);
            }

            var resultado = await service.CriarAsync(criarDto);
            return RedirectToAction(nameof(Index), resultado);
        }


        [Route("Deletar")]
         public async Task<IActionResult> DeletarAsync(int id)
        {
            var resultado = await service.DeletarAsync(id);
            return RedirectToAction(nameof(Index), resultado);
        }
    }
}
