using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Controllers.Teste;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Data.JogoDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : GenericController<Jogo, JogoCriarDto, JogoEditarDto>
    {
<<<<<<< HEAD
        private readonly IRepository<Clube> clubeRepository;

        public JogoController(IRepository<Jogo> repository, IService<Jogo, JogoCriarDto, JogoEditarDto> service, IRepository<Clube> clubeRepository) : base(repository, service)
        {
            this.clubeRepository = clubeRepository;
=======
        private readonly IRepository<Jogo> _jogoRepository;
        private readonly IRepository<Clube> _clubeRepository;
        private readonly AppDbContext _appDbContext;

        public JogoController(
            IRepository<Jogo> jogoRepository, 
            IRepository<Clube> clubeRepository,
            AppDbContext appDbContext)
        {
            _jogoRepository = jogoRepository;
            _clubeRepository = clubeRepository;
            _appDbContext = appDbContext;
>>>>>>> 02b49d4b40b79d4546ba1320617cf79b40382f6d
        }

        public override async Task<IActionResult> Criar()
        {
<<<<<<< HEAD
            var clubes = await clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            return View();
        }
=======
            var queryJogos = _appDbContext.Jogos
                .Include(j => j.ClubeA)
                .Include(j => j.ClubeB);

            var jogos = queryJogos.ToList();

            return View(jogos);
        }

        [Route("Criar")]
        public async Task<ActionResult> CriarAsync()
        {
            var clubes = await _clubeRepository.SelectAllAsync();
            ViewData["ListaClubes"] = clubes;
            return View();
        }

        [HttpPost, Route("Criar")]

        public async Task<ActionResult> CriarAsync(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                await _jogoRepository.AddAndSaveAsync(jogo);
                return RedirectToAction(nameof(Index));
            }

            return View(jogo);
        }

        [Route("Editar")]
        public async Task<ActionResult> Editar(Guid id)
        {
            var jogo = await _jogoRepository.FindAsync(id);
            if (jogo == null)
                return RedirectToAction(nameof(Index));

            return View(jogo);
            
        }

        [HttpPost, Route("Editar")]
        public async Task<ActionResult> Editar(Jogo jogo)
        {
            var jogoBanco = await _jogoRepository.FindAsync(jogo.Id);

            jogoBanco.ClubeAId = jogo.ClubeAId;
            jogoBanco.ClubeBId = jogo.ClubeBId;
            jogoBanco.ClubeA = jogo.ClubeA;
            jogoBanco.ClubeB = jogo.ClubeB;

            jogoBanco.InicioJogo = jogo.InicioJogo;

            await _jogoRepository.UpdateAndSaveAsync(jogoBanco);

            return RedirectToAction(nameof(Index));
        }

        [Route("Deletar")]
        public async Task<ActionResult> Deletar(Guid id)
        {
            var jogo = await _jogoRepository.FindAsync(id);

            if (jogo == null)
                return RedirectToAction(nameof(Index));
            return View(jogo);
                        
        }


        [HttpDelete, Route("Deletar")]
        public async Task<ActionResult> Deletar(Jogo jogo)
        {
            var jogoBanco = await _jogoRepository.FindAsync(jogo.Id);
            await _jogoRepository.RemoveAndSaveAsync(jogoBanco);
            return RedirectToAction(nameof(Index));
        }

        [Route("Detalhes")]
        public async Task<ActionResult> Detalhar(Guid id)
        {
            var jogo = await _jogoRepository.FindAsync(id);
            if (jogo == null) 
                return RedirectToAction(nameof(Index));

            return View(jogo);
        }
>>>>>>> 02b49d4b40b79d4546ba1320617cf79b40382f6d
    }
}
