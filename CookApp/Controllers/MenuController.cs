using CookApp.BLL.Dtos.MenuDto;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductService _productService;

        public MenuController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMenu()
        {
            var products = await _productService.GetProducts();
            var menuDtoList = products.Select(product => new MenuDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Description = product.Description,
                ImageId = product.Image.Id,
                FileName = product.Image.FileName,
                Price = product.Price
            }).ToList();

            return Json(menuDtoList);
        }
    }
}
