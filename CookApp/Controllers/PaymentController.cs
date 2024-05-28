using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CookApp.API.Controllers
{
    [CustomAuthorize("Administrator", "Client")]
    public class PaymentController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        public PaymentController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;

        }
        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            var order = await _orderService.CurrentOrder();
            order.OrderStatus = Entity.Enums.OrderStatus.Completed;
            order.PaymentStatus = Entity.Enums.PaymentStatus.PaymentByCard;
            await _orderService.UpdateOrder(order.Id, order);
            await _cartService.CloseCurrentCart();

            return RedirectToAction("Confirmation");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCashPayment()
        {

            var order = await _orderService.CurrentOrder();
            order.OrderStatus = Entity.Enums.OrderStatus.Completed;
            order.PaymentStatus = Entity.Enums.PaymentStatus.PaymentAtRestaurant;

            await _orderService.UpdateOrder(order.Id, order);
            await _cartService.CloseCurrentCart();
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
     
    }

}
