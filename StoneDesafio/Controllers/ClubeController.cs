using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ClubeController : Controller
    {
        private readonly AppDbContext _context;


        public ClubeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult IndexAsync()
        {
            var clubes = _context.Clubes.ToList();
            return View(clubes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Clube clube)
        {
            if (ModelState.IsValid)
            {
                _context.Clubes.Add(clube);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexAsync));
            }
            return View(clube);
        }

        public IActionResult Editar(int id)
        {
            var clube = _context.Clubes.Find(id);
            if (clube == null)
                return RedirectToAction(nameof(IndexAsync));

            return View(clube);
        }

        [HttpPost]
        public IActionResult Editar(Clube clube)
        {
            var clubeBanco = _context.Clubes.Find(clube.Id);

            clubeBanco.Nome = clube.Nome;
            clubeBanco.Descricao = clube.Descricao;
            clubeBanco.UrlFoto = clube.UrlFoto;

            _context.Clubes.Update(clubeBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(IndexAsync));
        }

        public IActionResult Detalhes(int id)
        {
            var clube = _context.Clubes.Find(id);

            if (clube == null)
                return RedirectToAction(nameof(IndexAsync));

            return View(clube);
        }

        public IActionResult Deletar(int id)
        {
            var clube = _context.Clubes.Find(id);
            if (clube == null)
                return RedirectToAction(nameof(IndexAsync));

            return View(clube);
        }

        [HttpPost]
        public IActionResult Deletar(Clube clube)
        {
            var clubeBanco = _context.Clubes.Find(clube.Id);

            _context.Clubes.Remove(clubeBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(IndexAsync));
        }
    }
}
