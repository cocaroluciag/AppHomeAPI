using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
