using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    public class CarritoProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
