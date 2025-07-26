using HandMadeCakes.Models.Enums;
using HandMadeCakes.Services.Product;
using HandMadeCakes.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HandMadeCakes.Controllers
{
    //public area (controller)
    public class StoreController : Controller
    {
        private readonly IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Store?category=Sweet
        public async Task<IActionResult> Index(string? category)
        {
            if (string.IsNullOrEmpty(category))
            {
                var allProducts = await _productService.GetAllAsync();
                return View(allProducts);
            }

            if (!Enum.TryParse<ProductCategory>(category, true, out var parsedCategory))
            {
                return NotFound();
            }

            var productsByCategory = await _productService.GetByCategoryAsync(parsedCategory);
            return View(productsByCategory);
        }
    }
}
