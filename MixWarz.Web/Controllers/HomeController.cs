using Microsoft.AspNetCore.Mvc;
using MixWarz.Web.Models;
using System.Diagnostics;

namespace MixWarz.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }       
    }
}
