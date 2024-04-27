using CookApp.BLL.Dtos.ProductDto;
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
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task AddNewProduct(ProductDto productDto)
        {
            Product newProduct = new Product
            {
                Description = productDto.Description,
                Price = productDto.Price,
                Name = productDto.Name,
                ImageName = productDto.ImageName
            };

           await _productRepository.AddAsync(newProduct);
        }

        public async Task DeleteProduct(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<IQueryable<Product>> GetProducts()
        {
            var products = _productRepository.GetAllAsyncQuery();
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }
        public async Task UpdateProduct(int productId, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ImageName = productDto.ImageName;

            await _productRepository.Update(product);
        }
    }
}
