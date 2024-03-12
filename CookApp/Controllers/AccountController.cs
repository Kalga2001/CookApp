using CookApp.BLL.IServices;
using CookApp.Dtos.AccountDtos;
using Microsoft.AspNetCore.Mvc;



    namespace CookApp.Controllers
    {
        public class AccountController : Controller
        {
            private readonly IAccountService _accountService;

            public AccountController(IAccountService accountService)
            {
                _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(LoginDto login)
            {
                if (!ModelState.IsValid)
                {
                    return View(login);
                }

                var user = await _accountService.Login(login);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(login);
                }

                return RedirectToAction("Index", "Home");
            }

            [HttpGet]
            public IActionResult Registration()
            {
                return View();
            }

            [HttpPost]
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
        }

}
