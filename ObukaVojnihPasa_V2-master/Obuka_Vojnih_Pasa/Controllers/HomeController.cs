using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Obuka_Vojnih_Pasa.Models;

namespace Obuka_Vojnih_Pasa.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message)
        {
            if (message == null) message = string.Empty;
            ViewBag.Message = message;
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }


        public IActionResult ServerError()
        {
            return View();
        }
        public IActionResult PageNotFound(string message)
        {
            ViewBag.Message = message;
            return View();
        }


        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

    }
}
