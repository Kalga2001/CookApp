using CookApp.BLL.Dtos.OrderDto;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly ICartService _cartService;
        public OrderService(IGenericRepository<Order> orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        public async Task AddOrder(OrderDto orderDto)
        {
            int currentCartId = await _cartService.GetAvailableCart();
 
            Order order = new Order
            {
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                PaymentStatus = orderDto.PaymentStatus,
                OrderStatus = orderDto.OrderStatus,
                TableId = orderDto.TableId,
                CartId = currentCartId
            };

            await _orderRepository.AddAsync(order);
        }

        public async Task DeleteOrder(int orderId)
        {
             await _orderRepository.DeleteAsync(orderId);
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            Order order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                return new OrderDto
                {
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    PaymentStatus = order.PaymentStatus,
                    OrderStatus = order.OrderStatus,
                    TableId = order.TableId,
                    CartId = order.CartId
                };
            }
            return null;
        }

        public async Task<IQueryable<OrderDto>> GetOrders()
        {
            IQueryable<Order> orders = _orderRepository.GetAllAsyncQuery();
            return orders.Select(order => new OrderDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                TableId = order.TableId,
                CartId = order.CartId
            });
        }

        public async Task UpdateOrder(int orderId, OrderDto orderDto)
        {
            Order existingOrder = await _orderRepository.GetByIdAsync(orderId);

            existingOrder.OrderDate = orderDto.OrderDate;
            existingOrder.TotalAmount = orderDto.TotalAmount;
            existingOrder.PaymentStatus = orderDto.PaymentStatus;
            existingOrder.OrderStatus = orderDto.OrderStatus;
            existingOrder.TableId = orderDto.TableId;

            await _orderRepository.Update(existingOrder);

        }
    }

}
