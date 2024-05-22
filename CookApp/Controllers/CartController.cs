using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService; 
        }
        [CustomAuthorize("Client")]
        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCart();
            return View(cartItems);
        }

    }
}
