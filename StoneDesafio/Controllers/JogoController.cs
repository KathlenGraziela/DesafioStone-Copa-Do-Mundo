using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : GenericController<Jogo, JogoCriarDto, JogoEditarDto>
    {
        private readonly IRepository<Clube> clubeRepository;

        public JogoController(IRepository<Jogo> repository, IService<Jogo, JogoCriarDto, JogoEditarDto> service, IRepository<Clube> clubeRepository) : base(repository, service)
        {
            this.clubeRepository = clubeRepository;
        }

        public override async Task<IActionResult> Criar()
        {
            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            return View();
        }

        public override async Task<IActionResult> Index(MensagemRota<Jogo> msg = null)
        {
            var jogos = await repository.GetSet().Include(j => j.ClubeA).Include(j => j.ClubeB).ToListAsync();
            return View(jogos);
        }

        public override async Task<IActionResult> Editar(int id)
        {
            var jogo = await repository.GetSet()
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeB)
                .FirstOrDefaultAsync(j => j.Id == id);

            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            if (jogo == null)
            {
                var msg = new MensagemRota<Clube>(MensagemResultado.Falha, $"Clube nao encontrado!");
                return RedirectToAction(nameof(Index), msg);
            }
            return View(jogo);
        }
    }
}
