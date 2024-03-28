using CookApp.Entity.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class AdminController : Controller
    {
 
        public IActionResult Index()
        {
            return View();
        }
    }
}
