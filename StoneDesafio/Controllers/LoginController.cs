using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Business.Repositorys;
using StoneDesafio.Business.Services;
using StoneDesafio.Data.AdministradorDtos;
using StoneDesafio.Models;
using StoneDesafio.Models.Utils;

namespace StoneDesafio.Controllers
{
    public class LoginController : AppBaseController
    {
        private readonly LoginService loginService;
        private readonly IRepository<Administrador> repository;
        private readonly ModelConverter modelConverter;

        public LoginController(LoginService loginService, IRepository<Administrador> repository, ModelConverter modelConverter)
        {
            this.loginService = loginService;
            this.repository = repository;
            this.modelConverter = modelConverter;
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

                return View(createDto);
            }
            return RedirectPermanent(nameof(Index));
        }

        [Authorize]
        [Route("Editar")]
        public async Task<IActionResult> EditarAsync()
        {
            var id = int.Parse(User.Identities.First().Claims.First().Value);
            var administrador = await repository.FindAsync(id);
            var admDto = modelConverter.Convert<AdministradorEditarDto>(administrador);
            return View(admDto);
        }

        [Authorize]
        [Route("Editar")]
        [HttpPost]
        public async Task<IActionResult> EditarAsync(AdministradorEditarDto editarDto)
        {
            if (!ModelState.IsValid)
            {
                editarDto.Senha = "";
                return View(editarDto);
            }

            var result = await loginService.Editar(editarDto);
            if (result.Resultado != MensagemResultado.Sucesso)
            {
                return View(editarDto);
            }
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        [Route("Sair")]
        public IActionResult Sair()
        {
            HttpContext.SignOutAsync();
            return RedirectToActionPermanent(nameof(Index));
        }

        [Authorize]
        [Route("Deletar")]
        virtual public async Task<IActionResult> DeletarAsync(int id)
        {
            await HttpContext.SignOutAsync();
            var resultado = await loginService.DeletarAsync(id);
            return RedirectToAction(nameof(Index), resultado);
        }
    }
}
