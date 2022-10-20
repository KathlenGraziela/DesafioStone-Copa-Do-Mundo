using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class LoginController : AppBaseController
    {
        private readonly LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        public IActionResult Index([FromQuery] string msg = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(AdministradorLoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), login);
            }

            var result = await loginService.Login(login);
            ViewBag.msg = result.Mensagem;

            if (result.Resultado == MensagemResultado.Falha)
            {
                login.Senha = null;
                return View(nameof(Index), login);
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Sucessos);
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }

        [Route("Cadastrar")]
        public IActionResult CadastrarAsync()
        {
            return View();   
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> CadastrarAsync(AdministradorCriarDto createDto)
        {
            if (!ModelState.IsValid)
            {
                createDto.Senha = "";
                return View(createDto);
            }

            var result = await loginService.Cadastrar(createDto);
            if(result.Resultado != MensagemResultado.Sucesso)
            {
                ViewBag.msg = result.Mensagem;
                return View(createDto);
            }
            return RedirectPermanent(HttpContext.Request.Headers.Referer);
        }


        [Route("Sair")]
        public IActionResult Sair()
        {
            HttpContext.SignOutAsync();
            return RedirectToActionPermanent(nameof(Index));
        }
    }
}
