using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface ICartItemService
    {
        Task AddToCartItem(CartItem cartItem);
        Task DeleteCartItemById(int id);
        Task<CartItem> GetByIdCartItem(int id);
        Task<IQueryable<CartItem>> GetAllCartItems();
    }
}
