using CookApp.BLL.Dtos.ProductDto;
using CookApp.Entity.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IProductService
    {
        Task<IQueryable<Product>> GetProducts();
        Task<IQueryable<Image>> GetAllImagesExceptCurrentProductImage(int productId);
        Task<Product> GetProductById(int productId);
        Task AddNewProduct(ProductDto productDto);
        Task UpdateProduct(int productId, ProductDto productDto);
        Task DeleteProduct(int productId);
    }
}
