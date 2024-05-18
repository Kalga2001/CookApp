using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartItemController(ICartItemService cartItemService, IProductService productService, ICartService cartService)
        {
            _cartItemService = cartItemService;
            _productService = productService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //create for user cart id
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody]CartItem model)
        {
            int cartId = await _cartService.GetAvailableCart();

            if(cartId < 1)
                 cartId = await _cartService.CreateCartAsync();

            var product = await _productService.GetProductById(model.ProductId);

            var cartItem = new CartItem
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = product.Price * model.Quantity,
                AddedAt = DateTime.UtcNow
            };
 
            await _cartService.AddCartItemToCartAsync(cartId, cartItem);

            return Ok(cartItem);

        }
    }
}
