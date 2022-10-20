using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data;
using StoneDesafio.Entities;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Controllers
{
    public class FaseController : AppBaseController
    {
        
        private readonly IRepository<FaseCampeonato> repository;
        private readonly FaseService faseService;
        private readonly ModelConverter modelConverter;


        public FaseController(IRepository<FaseCampeonato> repository, ModelConverter modelConverter, FaseService faseService)
        {
            this.repository = repository;
            this.modelConverter = modelConverter;
            this.faseService = faseService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var FasesCampeonato = await repository.SelectAllAsync();
            return View(FasesCampeonato);
        }

        public ActionResult Criar() => View();

        [HttpPost]

        public async Task<ActionResult> CriarAsync(FaseCriarDto FaseDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            var fase = modelConverter.Convert<FaseCampeonato>(FaseDto);
            await repository.AddAndSaveAsync(fase);


            return View(fase);
        }

        public async Task<ActionResult> EditarAsync(int id)
        {
            var fase = await repository.FindAsync(id);
            if (fase == null)
                return RedirectToAction(nameof(IndexAsync));

            return View(fase);
            
        }

        [HttpPost]
        public async Task<ActionResult> EditarAsync(int id,  FaseEditarDto editarDto)
        {
            var fase = await faseService.EditarAsync(id, editarDto);


            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<ActionResult> DeletarViewAsync(int id)
        {
            var fase = await repository.FindAsync(id);

            if (fase == null)
                return RedirectToAction(nameof(IndexAsync));
            return View(fase);
                        
        }

        [HttpDelete]
        public async Task<ActionResult> DeletarAsync(int id)
        {
            await faseService.DeletarAsync(id);
            return RedirectToAction(nameof(IndexAsync));
        }

        public async Task<ActionResult> DetalharAsync(Guid id)
        {
            var fase = await repository.FindAsync(id);
            if (fase == null) 
                return RedirectToAction(nameof(IndexAsync));

            return View(fase);
        }
            

    }
}
