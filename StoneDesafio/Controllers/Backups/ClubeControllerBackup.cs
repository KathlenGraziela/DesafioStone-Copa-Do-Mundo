using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers.Backups
{
    public class ClubeControllerBackup : AppBaseController
    {
        private readonly IRepository<Clube> _clubeRepository;
        private readonly ClubeService clubeService;

        public ClubeControllerBackup(IRepository<Clube> clubeRepository, ClubeService clubeService)
        {
            _clubeRepository = clubeRepository;
            this.clubeService = clubeService;
        }

        public async Task<IActionResult> Index(MensagemRota<Clube> msg)
        {
            var clubes = await _clubeRepository.SelectAllAsync();

            if (msg.Mensagem != null)
            {
                ViewBag.Mensagem = msg;
            }
            return View(clubes);
        }

        [Route("Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost, Route("Criar")]
        public async Task<IActionResult> CriarAsync(ClubeCriarDto criarDto)
        {
            if (!ModelState.IsValid)
            {
                return View(criarDto);
            }

            var resultado = await clubeService.CriarAsync(criarDto);
            return RedirectToAction(nameof(Index), resultado);
        }

        [Route("Editar")]
        public async Task<IActionResult> EditarAsync(int id)
        {
            var clube = await _clubeRepository.FindAsync(id);
            if (clube == null)
                return RedirectToAction(nameof(Index), new MensagemRota<Clube>(MensagemResultado.Falha, "Clube nao encontrado!"));

            return View(clube);
        }

        [HttpPost, Route("Editar")]
        public async Task<IActionResult> EditarAsync(ClubeEditarDto editarDto)
        {
            var resultado = await clubeService.EditarAsync(editarDto);

            return RedirectToAction(nameof(Index), resultado);
        }

        [Route("Detalhes")]
        public async Task<IActionResult> DetalhesAsync(int id)
        {
            var clube = await _clubeRepository.FindAsync(id);

            if (clube == null)
                return RedirectToAction(nameof(Index), new MensagemRota<Clube>(MensagemResultado.Falha, "Clube nao encontrado!"));

            return View(clube);
        }

        [Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var resultado = await clubeService.DeletarAsync(id);
            return RedirectToAction(nameof(Index), resultado);
        }
    }
}
