using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class LoginController : AppBaseController
    {
        private readonly IRepository<Administrador> repository;
        private readonly AdministradorService administradorService;

        public LoginController(IRepository<Administrador> repository, AdministradorService administradorService)
        {
            this.repository = repository;
            this.administradorService = administradorService;
        }

        public IActionResult Index([FromQuery] string? msg = null)
        {
            ViewBag.msg = msg;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(AdministradorLoginDto login)
        {
            var administrador = await repository.FindFirstAsync(a => a.Email == login.Email);
            if (!ModelState.IsValid || administrador == null || !CriptografiaService.Verficar(login.Senha, administrador.Senha))
            {
                TempData["Email"] = login.Email;
                return RedirectToAction(nameof(Index), new { msg = "Usuário e/ou Senha inválidos. Por favor, tente novamente." });
            }
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync(AdministradorCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Email"] = createDto.Email;
                TempData["Nome"] = createDto.Email;
                return RedirectToAction(nameof(Index), new { msg = "Erro. Por favor, tente novamente." });
            }
            await administradorService.CriarAsync(createDto);
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }
    }
}
