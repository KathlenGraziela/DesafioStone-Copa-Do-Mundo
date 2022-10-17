using Microsoft.AspNetCore.Mvc;
using StoneDesafio.Models;

namespace StoneDesafio.Controllers
{
    public class LoginController : Controller
    {
        
        public LoginController()
        {
            //espaço para implementar o código que possa validar os usuários cadastrados.
            //solicitar acompanhamento do samuel para juntar com o administrador.
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (login.Email == "daniel" && login.Senha == "daniel") //login de teste para logar.
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou Senha inválidos. Por favor, tente novamente.";
                }
                return View("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
