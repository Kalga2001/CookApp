using CookApp.BLL.Dtos.CartDto;
using CookApp.BLL.Dtos.CartItemDto;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly ICartService _cartService;
        public CartItemService(IGenericRepository<CartItem> cartItemRepository, ICartService cartService)
        {
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
        }

        public async Task AddToCartItem(CartItem cartItem)
        {
           await _cartItemRepository.AddAsync(cartItem);    
        }
 
        public async Task DeleteCartItemById(int id)
        {
            var cartToDelete = await _cartItemRepository.GetByIdAsync(id);
            if (cartToDelete != null)
            {
                await _cartItemRepository.DeleteAsync(cartToDelete);
            }
        }

        public async Task<CartItem> GetByIdCartItem(int id)
        {
            CartItem cartItem = await _cartItemRepository.GetByIdAsync(id);
            return cartItem;
        }

        public async Task<IQueryable<CartItem>> GetAllCartItems()
        {
            return _cartItemRepository.GetAllAsyncQuery();
        }

    }

}
