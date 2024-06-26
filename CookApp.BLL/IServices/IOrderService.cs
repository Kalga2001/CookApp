﻿using CookApp.BLL.Dtos.OrderDto;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IOrderService
    {
        Task<IQueryable<OrderDto>> GetOrders();
        Task<OrderDto> GetOrder(int orderId);
        Task<Order> GetOrderEntity(int orderId);
        Task AddOrder(OrderDto orderDto);
        Task UpdateOrder(int orderId , Order order);
        Task DeleteOrder(int orderId);
        Task<Order> CurrentOrder();
    }
}
