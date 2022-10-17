using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Rendering;
=======
using StoneDesafio.Business.Repositorys;
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : Controller
    {
<<<<<<< HEAD
        private readonly AppDbContext _context;

        public JogoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jogos = _context.Jogos.ToList();
=======
        private readonly IRepository<Jogo> repository;

        public JogoController(IRepository<Jogo> repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var jogos = await repository.SelectAllAsync();
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640
            return View(jogos);
        }

        public ActionResult Criar()
        {
<<<<<<< HEAD
            var clubes = _context.Clubes.ToList();
            ViewData["ListaClubes"] = clubes;
=======
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640
            return View();
        }

        [HttpPost]

<<<<<<< HEAD
        public ActionResult Criar(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                _context.Jogos.Add(jogo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(jogo);
                              
                      
        }

        public ActionResult Editar(int id)
        {
            var jogo = _context.Jogos.Find(id);
            if (jogo == null)
                return RedirectToAction(nameof(Index));
=======
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
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640

            return View(jogo);
            
        }

        [HttpPost]
<<<<<<< HEAD
        public ActionResult Editar(Jogo jogo)
        {
            var jogoBanco = _context.Jogos.Find(jogo.Id);
=======
        public async Task<ActionResult> EditarAsync(Jogo jogo)
        {
            var jogoBanco = await repository.FindAsync(jogo.Id);
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640

            jogoBanco.ClubeA = jogo.ClubeA;
            jogoBanco.ClubeB = jogo.ClubeB;
            jogoBanco.GolsClubeA = jogo.GolsClubeA;
            jogoBanco.GolsClubeB = jogo.GolsClubeB;
            jogoBanco.InicioJogo = jogo.InicioJogo;
            jogoBanco.FimJogo = jogo.FimJogo;

<<<<<<< HEAD
            _context.Jogos.Update(jogoBanco);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Deletar(int id)
        {
            var jogo = _context.Jogos.Find(id);

            if (jogo == null)
                return RedirectToAction(nameof(Index));
=======
            await repository.UpdateAndSaveAsync(jogoBanco);

            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<ActionResult> DeletarAsync(int id)
        {
            var jogo = await repository.FindAsync(id);

            if (jogo == null)
                return RedirectToAction(nameof(IndexAsync));
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640
            return View(jogo);
                        
        }

        [HttpDelete]
<<<<<<< HEAD
        public ActionResult Deletar(Jogo jogo)
        {
            var jogoBanco = _context.Jogos.Find(jogo.Id);
            _context.Jogos.Remove(jogoBanco);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detalhar(int id)
        {
            var jogo = _context.Jogos.Find(id);
            if (jogo == null);
            return RedirectToAction(nameof(Index));
=======
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
>>>>>>> 5c43f3532456e23dc1cd80985895bdbabae14640

            return View(jogo);
        }
            

    }
}
