using CookApp.BLL.Dtos.CartDto;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;
 
        public CartService(IGenericRepository<Cart> cartRepository, IGenericRepository<CartItem> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<int> GetAvailableCart()
        {
            var inProgressCart = await _cartRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.CartState == Entity.Enums.CartState.InProgress);

            return inProgressCart?.Id ?? 0;
        }

        public async Task<IQueryable<CartDto>> GetCart()
        {
            int cartId = await GetAvailableCart();

            var cartItems = _cartItemRepository.GetAllAsyncQuery()
                                               .Where(x => x.CartId == cartId)
                                               .GroupBy(x => new { x.ProductId, x.Product.Name }) 
                                               .Select(group => new CartDto
                                               {
                                                   ProductId = group.Key.ProductId,
                                                   ProductName = group.Key.Name, 
                                                   Quantity = group.Sum(x => x.Quantity),
                                                   TotalPrice = group.Sum(x => x.TotalPrice),
                                                   UnitPrice = group.Sum(x => x.TotalPrice) / group.Sum(x => x.Quantity)
                                               });

            return cartItems;
        }



        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _cartRepository.GetByIdAsync(cartId);
        }

        public async Task<int> CreateCartAsync()
        {
            var cart = new Cart
            {
                CreatedAt = DateTime.UtcNow,
                Items = new List<CartItem>(),
                CartState = Entity.Enums.CartState.InProgress
            };

            await _cartRepository.AddAsync(cart);
            return cart.Id;  
        }

        public async Task AddCartItemToCartAsync(int cartId, CartItem cartItem)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);

 
            if (cart.Items == null)
            {
                cart.Items = new List<CartItem>();
            }

  
            cart.Items.Add(cartItem);

    
            await _cartRepository.Update(cart);
        }

    }

}
