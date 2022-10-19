using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Controllers.Teste;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ClubeController : GenericController<Clube, ClubeCriarDto, ClubeEditarDto>
    {
        public ClubeController(IRepository<Clube> repository, IService<Clube, ClubeCriarDto, ClubeEditarDto> service) : base(repository, service)
        {
<<<<<<< HEAD
=======
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
        public async Task<IActionResult> EditarAsync(Guid id)
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
        public async Task<IActionResult> DetalhesAsync(Guid id)
        {
            var clube = await _clubeRepository.FindAsync(id);

            if (clube == null)
                return RedirectToAction(nameof(Index));

            return View(clube);
        }

        [Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(Guid id)
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
>>>>>>> 02b49d4b40b79d4546ba1320617cf79b40382f6d
        }
    }
}
