﻿using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
