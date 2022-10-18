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

        public LoginController(IRepository<Administrador> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index([FromQuery] string? msg = null)
        {
            ViewBag.msg = msg;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(AdministradorLoginDto login)
        {
            var administrador = await repository.SelectFirstAsync(a => a.Email == login.Email);
            if (!ModelState.IsValid || administrador == null || !CriptografiaService.Verficar(login.Senha, administrador.Senha))
            {
                TempData["Email"] = login.Email;
                return RedirectToAction(nameof(Index), new { msg = "Usuário e/ou Senha inválidos. Por favor, tente novamente." });
            }
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }
    }
}
