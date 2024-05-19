using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookApp.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _userService;
        private readonly IReservationService _reservationService;
        public HomeController(ILogger<HomeController> logger, IAccountService userService, IReservationService reservationService)
        {
            _logger = logger;
            _userService = userService;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
             
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
 
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Event()
        {
            return View();
        }

        public IActionResult SpecialDish()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
