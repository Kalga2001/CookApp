
using CookApp.BLL.Dtos.ImageDtos;
using CookApp.BLL.Dtos.ProductDto;
using CookApp.BLL.Dtos.RoleManagementDto;
using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using CookApp.API.Extension;

namespace CookApp.API.Controllers
{
    [CustomAuthorize("Administrator")]
    public class ProductsController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IProductService _productService;
        private readonly string _imageFolderPath;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IImageService imageService, IProductService productService, IWebHostEnvironment hostEnvironment)
        {
            _imageService = imageService;
            _productService = productService;
            _hostEnvironment = hostEnvironment;
            _imageFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            var productDtos = products.Select(product => new ProductDto
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageId = product.Image.Id,
                FileName = product.Image.FileName,
                ContentType = product.Image.ContentType,
                Size = product.Image.Size
            });

            return View(productDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var allImagesExceptCurrent = await _productService.GetAllImagesExceptCurrentProductImage(id);

            var response = new
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Images = allImagesExceptCurrent.Select(image => new
                {
                    ImageId = image.Id,
                    FileName = image.FileName,
                    ContentType = image.ContentType,
                    Data = Convert.ToBase64String(image.Data) 
                })
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromForm] ProductDto productDto)
        {
            try
            {
                if (productDto == null || productDto.Data == null)
                {
                    return BadRequest("Invalid product data or missing image");
                }

                string extension = GetFileExtension(productDto.ContentType); // Get file extension from content type
                string fileName = Guid.NewGuid().ToString() + extension;

                string imagePath = Path.Combine(_imageFolderPath, fileName);

                await System.IO.File.WriteAllBytesAsync(imagePath, productDto.Data);

                productDto.FileName = fileName;

                await _productService.AddNewProduct(productDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating a new product: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                await _productService.UpdateProduct(productDto.ProductId, productDto);
                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating product: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting the product: " + ex.Message);
            }
        }
        private string GetFileExtension(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "image/gif":
                    return ".gif";
                default:
                    return ".jpg"; 
            }
        }
    }

}
