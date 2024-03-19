using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookApp.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _userService;

        public HomeController(ILogger<HomeController> logger, IAccountService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            string loggedInUserEmail = _userService.GetCurrentUserEmail(); // Получаем адрес электронной почты текущего пользователя
            ViewData["LoggedInUserEmail"] = loggedInUserEmail; // Передаем адрес электронной почты в представление через ViewData
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
