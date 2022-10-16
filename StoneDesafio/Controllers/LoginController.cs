using Microsoft.AspNetCore.Mvc;

namespace StoneDesafio.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
