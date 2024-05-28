using CookApp.BLL.Dtos.OrderDto;
using CookApp.BLL.IServices;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    [CustomAuthorize("Administrator")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IReservationService _reservationService;
        public OrdersController(IOrderService orderService, IReservationService reservationService)
        {
            _orderService = orderService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrders();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var tables = await _reservationService.GetTables();
            return Json(tables);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromForm] OrderDto orderDto)
        {
            var existingOrder = await _orderService.GetOrderEntity(orderDto.OrderId);

            existingOrder.TotalAmount = orderDto.TotalAmount;
            existingOrder.PaymentStatus = orderDto.PaymentStatus;
            existingOrder.TableId = orderDto.TableId;

            await _orderService.UpdateOrder(orderDto.OrderId, existingOrder);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {

            await _orderService.DeleteOrder(orderId);
            return Ok();

        }
    }
}
