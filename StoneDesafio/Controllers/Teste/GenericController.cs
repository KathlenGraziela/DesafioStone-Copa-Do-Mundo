using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.ClubeDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers.Teste
{
    public class GenericController<TModel, TCriarDto, TEditarDto> : AppBaseController where TModel : class 
    {
        private readonly IRepository<TModel> repository;
        private readonly IService<TModel, TCriarDto, TEditarDto> service;

        public GenericController(IRepository<TModel> repository, IService<TModel, TCriarDto, TEditarDto> service)
        {
            this.repository = repository;
            this.service = service;
        }

        virtual public async Task<IActionResult> Index(MensagemRota<TModel> msg = null)
        {
            var models = await repository.SelectAllAsync();

            if (msg.Mensagem != null)
            {
                ViewBag.Mensagem = msg;
            }
            return View(models);
        }

        [Route("Criar")]
        virtual public async Task<IActionResult> Criar() =>
            await Task.FromResult(View());

        [HttpPost, Route("Criar")]
        virtual public async Task<IActionResult> CriarPost(TCriarDto criarDto)
        {
            if (!ModelState.IsValid)
            {
                return View(criarDto);
            }

            var resultado = await service.CriarAsync(criarDto);
            return RedirectToAction(nameof(Index), resultado);
        }

        [Route("Editar")]
        virtual public async Task<IActionResult> Editar(int id)
        {
            var model = await repository.FindAsync(id);
            if (model == null)
                return RedirectToAction(nameof(Index), new MensagemRota<TModel>(MensagemResultado.Falha, $"{typeof(TModel).Name} nao encontrado!"));

            return View(model);
        }

        [HttpPost, Route("Editar")]
        virtual public async Task<IActionResult> EditarPost(TEditarDto editarDto)
        {
            var resultado = await service.EditarAsync(editarDto);

            return RedirectToAction(nameof(Index), resultado);
        }

        [Route("Detalhes")]
        virtual public async Task<IActionResult> DetalhesAsync(int id)
        {
            var clube = await repository.FindAsync(id);
            if (clube == null)
                return RedirectToAction(nameof(Index), new MensagemRota<TModel>(MensagemResultado.Falha, $"{typeof(TModel).Name} nao encontrado!"));

            return View(clube);
        }

        [Route("Deletar")]
        virtual public async Task<IActionResult> DeletarAsync(int id)
        {
            var resultado = await service.DeletarAsync(id);
            return RedirectToAction(nameof(Index), resultado);
        }
    }
}
