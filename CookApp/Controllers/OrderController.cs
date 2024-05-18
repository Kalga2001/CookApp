using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
