using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class ResultadoControllerBackup : AppBaseController
    {
        private readonly IRepository<Resultado> _resultadoRepository;

        public ResultadoControllerBackup(IRepository<Resultado> resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var resultados = await _resultadoRepository.SelectAllAsync();
            return View(resultados);
        }

        [Route("Criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost, Route("Criar")]
        public async Task<IActionResult> CriarAsync(Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                await _resultadoRepository.AddAndSaveAsync(resultado);
                return RedirectToAction(nameof(Index));
            }
            return View(resultado);
        }

        [Route("Editar")]
        public async Task<IActionResult> EditarAsync(int id)
        {
            var resultado = await _resultadoRepository.FindAsync(id);
            if (resultado == null)
                return RedirectToAction(nameof(Index));

            return View(resultado);
        }

        [HttpPost, Route("Editar")]
        public async Task<IActionResult> EditarAsync(Resultado resultado)
        {
            var resultadoBanco = await _resultadoRepository.FindAsync(resultado.Id);

            resultadoBanco.GolsClubeA = resultado.GolsClubeA;
            resultadoBanco.GolsClubeB = resultado.GolsClubeB;
            resultadoBanco.FimJogo = resultado.FimJogo;

            await _resultadoRepository.UpdateAndSaveAsync(resultadoBanco);

            return RedirectToAction(nameof(Index));
        }

        [Route("Detalhes")]
        public async Task<IActionResult> DetalhesAsync(int id)
        {
            var resultado = await _resultadoRepository.FindAsync(id);

            if (resultado == null)
                return RedirectToAction(nameof(Index));

            return View(resultado);
        }

        [Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var resultado = await _resultadoRepository.FindAsync(id);
            if (resultado == null)
                return RedirectToAction(nameof(Index));

            return View(resultado);
        }

        [HttpPost, Route("Deletar")]
        public async Task<IActionResult> DeletarAsync(Resultado resultado)
        {
            var resultadoBanco = await _resultadoRepository.FindAsync(resultado.Id);

            await _resultadoRepository.RemoveAndSaveAsync(resultadoBanco);

            return RedirectToAction(nameof(Index));
        }
    }
}
