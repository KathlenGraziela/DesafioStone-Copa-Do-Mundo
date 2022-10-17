using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : Controller
    {
        private readonly IRepository<Jogo> repository;

        public JogoController(IRepository<Jogo> repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var jogos = await repository.SelectAllAsync();
            return View(jogos);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> CriarAsync(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                await repository.AddAndSaveAsync(jogo);
                return RedirectToAction(nameof(IndexAsync));
            }

            return View(jogo);
        }

        public async Task<ActionResult> EditarAsync(int id)
        {
            var jogo = await repository.FindAsync(id);
            if (jogo == null)
                return RedirectToAction(nameof(IndexAsync));

            return View(jogo);
            
        }

        [HttpPost]
        public async Task<ActionResult> EditarAsync(Jogo jogo)
        {
            var jogoBanco = await repository.FindAsync(jogo.Id);

            jogoBanco.ClubeA = jogo.ClubeA;
            jogoBanco.ClubeB = jogo.ClubeB;
            jogoBanco.GolsClubeA = jogo.GolsClubeA;
            jogoBanco.GolsClubeB = jogo.GolsClubeB;
            jogoBanco.InicioJogo = jogo.InicioJogo;
            jogoBanco.FimJogo = jogo.FimJogo;

            await repository.UpdateAndSaveAsync(jogoBanco);

            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<ActionResult> DeletarAsync(int id)
        {
            var jogo = await repository.FindAsync(id);

            if (jogo == null)
                return RedirectToAction(nameof(IndexAsync));
            return View(jogo);
                        
        }

        [HttpDelete]
        public async Task<ActionResult> DeletarAsync(Jogo jogo)
        {
            var jogoBanco = await repository.FindAsync(jogo.Id);
            await repository.RemoveAndSaveAsync(jogoBanco);
            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<ActionResult> DetalharAsync(int id)
        {
            var jogo = await repository.FindAsync(id);
            if (jogo == null) 
                return RedirectToAction(nameof(IndexAsync));

            return View(jogo);
        }
            

    }
}
