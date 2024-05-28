using CookApp.BLL.Dtos.OrderDto;
using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CookApp.API.Controllers
{
    [CustomAuthorize("Administrator", "Client")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCart();

            int cartId = await _cartService.GetAvailableCart();

            Order currentOrder = await _orderService.CurrentOrder();

            decimal totalPrice = 0;
            foreach (var cartItem in cartItems)
            {
                totalPrice += cartItem.TotalPrice;
            }
            
            if(currentOrder == null)
            {
                var orderDto = new OrderDto()
                {
                    TotalAmount = totalPrice,
                    OrderDate = DateTime.Now,
                    CartId = cartId,
                    PaymentStatus = Entity.Enums.PaymentStatus.NoPayment,
                    OrderStatus = Entity.Enums.OrderStatus.InProgress
                };
                await _orderService.AddOrder(orderDto);
            }
            else
            {
                currentOrder.TotalAmount = totalPrice;
                await _orderService.UpdateOrder(currentOrder.Id, currentOrder);
            }
            
            return View(cartItems);
        }

    }
}
