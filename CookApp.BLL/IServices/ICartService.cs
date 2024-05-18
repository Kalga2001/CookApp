using CookApp.BLL.Dtos.CartDto;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface ICartService
    {
        Task<IQueryable<CartDto>> GetCart();
        Task<int> GetAvailableCart();
        Task<Cart> GetCartByIdAsync(int cartId);
        Task<int> CreateCartAsync();
        Task AddCartItemToCartAsync(int cartId, CartItem cartItem);
    }
}
