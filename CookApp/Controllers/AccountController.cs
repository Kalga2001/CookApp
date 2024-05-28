using CookApp.BLL.Dtos.AccountDtos;
using CookApp.BLL.IServices;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;



namespace CookApp.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
       
        public AccountController(IAccountService accountService,ITokenService tokenService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _tokenService = tokenService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            User user = await _accountService.Login(login);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(login);
            }

            var token = _tokenService.CreateToken(user);

            HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(120) 
            });

            var roles = _accountService.GetRolesByUserID(user.Id);

            if (roles.Contains("Administrator"))
            {
                return RedirectToAction("Index","Admin");
            }
 
            return RedirectToAction("Index", "Home", new { token = token });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationDto registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            try
            {
                await _accountService.Registration(registration);
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(registration);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordDto);
            }

            await _accountService.ResetPassword(forgotPasswordDto);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            return Ok(); // или что-то другое в зависимости от вашей логики
        }
    }
}


