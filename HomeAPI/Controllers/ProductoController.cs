using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
