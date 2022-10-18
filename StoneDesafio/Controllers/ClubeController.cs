using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ClubeController : AppBaseController
    {
        private readonly IRepository<Clube> _clubeRepository;

        public ClubeController(IRepository<Clube> clubeRepository)
        {
            _clubeRepository = clubeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clubes = await _clubeRepository.SelectAllAsync();
            return View(clubes);
        }

        [Route("Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost, Route("Criar")]
        public async Task<IActionResult> CriarAsync(Clube clube)
        {
            if (ModelState.IsValid)
            {
                await _clubeRepository.AddAndSaveAsync(clube);
                return RedirectToAction(nameof(Index));
            }
            return View(clube);
        }

        [Route("Editar")]
        public async Task<IActionResult> EditarAsync(int id)
        {
            var clube = await _clubeRepository.FindAsync(id);
            if (clube == null)
                return RedirectToAction(nameof(Index));

            return View(clube);
        }

        [HttpPost, Route("Editar")]
        public async Task<IActionResult> EditarAsync(Clube clube)
        {
            var clubeBanco = await _clubeRepository.FindAsync(clube.Id);

            clubeBanco.Nome = clube.Nome;
            clubeBanco.Descricao = clube.Descricao;
            clubeBanco.UrlFoto = clube.UrlFoto;

            await _clubeRepository.UpdateAndSaveAsync(clubeBanco);

            return RedirectToAction(nameof(Index));
        }

        [Route("Detalhes")]
        public async Task<IActionResult> DetalhesAsync(int id)
        {
            var clube = await _clubeRepository.FindAsync(id);

            if (clube == null)
                return RedirectToAction(nameof(Index));

            return View(clube);
        }

        [Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var clube = await _clubeRepository.FindAsync(id);
            if (clube == null)
                return RedirectToAction(nameof(Index));

            return View(clube);
        }

        [HttpPost, Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(Clube clube)
        {
            var clubeBanco = await _clubeRepository.FindAsync(clube.Id);

            await _clubeRepository.RemoveAndSaveAsync(clubeBanco);

            return RedirectToAction(nameof(Index));
        }
    }
}
