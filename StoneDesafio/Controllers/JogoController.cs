using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class JogoController : Controller
    {
        private readonly AppDbContext _context;

        public JogoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jogos = _context.Jogos.ToList();
            return View(jogos);
        }

        public ActionResult Criar()
        {
            var clubes = _context.Clubes.ToList();
            ViewData["ListaClubes"] = clubes;
            return View();
        }

        [HttpPost]

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

            return View(jogo);
            
        }

        [HttpPost]
        public ActionResult Editar(Jogo jogo)
        {
            var jogoBanco = _context.Jogos.Find(jogo.Id);

            jogoBanco.ClubeA = jogo.ClubeA;
            jogoBanco.ClubeB = jogo.ClubeB;
            jogoBanco.GolsClubeA = jogo.GolsClubeA;
            jogoBanco.GolsClubeB = jogo.GolsClubeB;
            jogoBanco.InicioJogo = jogo.InicioJogo;
            jogoBanco.FimJogo = jogo.FimJogo;

            _context.Jogos.Update(jogoBanco);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Deletar(int id)
        {
            var jogo = _context.Jogos.Find(id);

            if (jogo == null)
                return RedirectToAction(nameof(Index));
            return View(jogo);
                        
        }

        [HttpDelete]
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

            return View(jogo);
        }
            

    }
}
