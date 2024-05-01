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
        private readonly IGenericRepository<Image> _imageRepository;
        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<Image> imageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }
        public async Task AddNewProduct(ProductDto productDto)
        {
            Product newProduct = new Product
            {
                Description = productDto.Description,
                Price = productDto.Price,
                Name = productDto.Name,
                Image = new Image
                {
                    FileName = productDto.FileName,
                    Data = productDto.Data,
                    ContentType = productDto.ContentType,
                    Size = productDto.Size
                }
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

            var product = _productRepository.GetAllAsyncQuery().Include(x => x.Image).FirstOrDefault(x => x.Id == productId);

            return product;
        }
        public async Task UpdateProduct(int productId, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            var image = await _imageRepository.GetByIdAsync(productDto.ImageId);

            if (product != null)
            {
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;

                product.Image = image;

                await _productRepository.Update(product);

            }
        }

        public async Task<IQueryable<Image>> GetAllImagesExceptCurrentProductImage(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var allImages = _imageRepository.GetAllAsyncQuery(); 

            if (product != null && product.Image != null)
            {
                allImages = allImages.Where(image => image.Id != product.Image.Id);
            }

            return allImages;
        }
    }
}
