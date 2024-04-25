using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.BLL.IServices;
using CookApp.Entity.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
       
    }
}
