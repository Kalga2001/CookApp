using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IProductService
    {
        Task<Product> GetProducts();
        Task AddNewProduct();
        Task UpdateProduct();
        Task DeleteProduct();
    }
}
