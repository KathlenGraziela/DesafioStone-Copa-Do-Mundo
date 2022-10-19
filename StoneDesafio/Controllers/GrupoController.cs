using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class GrupoController : AppBaseController
    {
      
        private readonly IRepository<Grupo> _grupoRepository;

        public GrupoController(IRepository<Grupo> grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var grupo = await _grupoRepository.SelectAllAsync();
            return View(grupo);
        }

        [Route("Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost, Route("Criar")]
        public async Task<IActionResult> CriarAsync(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                await _grupoRepository.AddAndSaveAsync(grupo);
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }

        [Route("Editar")]
        public async Task<IActionResult> EditarAsync(Guid id)
        {
            var grupo = await _grupoRepository.FindAsync(id);
            if (grupo == null)
                return RedirectToAction(nameof(Index));

            return View(grupo);
        }

        [HttpPost, Route("Editar")]
        public async Task<IActionResult> EditarAsync(Grupo grupo)
        {
            var grupoBanco = await _grupoRepository.FindAsync(grupo.Id);

            grupoBanco.Nome = grupo.Nome;

            await _grupoRepository.UpdateAndSaveAsync(grupoBanco);

            return RedirectToAction(nameof(Index));
        }

        [Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(Guid id)
        {
            var grupo = await _grupoRepository.FindAsync(id);
            if (grupo == null)
                return RedirectToAction(nameof(Index));

            return View(grupo);
        }

        [HttpPost, Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(Grupo grupo)
        {
            var grupoBanco = await _grupoRepository.FindAsync(grupo.Id);

            await _grupoRepository.RemoveAndSaveAsync(grupoBanco);

            return RedirectToAction(nameof(Index));
        }
    }
}
