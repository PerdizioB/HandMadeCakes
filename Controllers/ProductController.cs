using HandMadeCakes.Dto;
using HandMadeCakes.Models;
using HandMadeCakes.Models.Enums;
using HandMadeCakes.Services.Cake;
using HandMadeCakes.Services.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICakeInterface _cakeService;

        // 🔹 Ajuste: injetando CakeService também
        public ProductController(IProductService productService, ICakeInterface cakeService)
        {
            _productService = productService;
            _cakeService = cakeService;
        }

        // GET: /Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
                return View(product);
            }

            // 1️⃣ Cria o produto
            await _productService.CreateAsync(product);

            // 2️⃣ Se for Cake, criar CakeModel automático
            if (product.Category == ProductCategory.Cake)
            {
                var dto = new CakeCreateDto
                {
                    ProductId = product.Id,
                    Flavor = product.Name ?? "",
                    Description = product.Description ?? "",
                    Price = product.Price, // ✅ decimal compatível
                };

                // Não há coverFoto nem imagens extras nesse momento
                await _cakeService.CriarCake(dto, null, null);

                // Redireciona para editar Cake (opcional)
                return RedirectToAction("Edit", "Cake", new { id = product.Id });
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
                return View(product);
            }

            var updated = await _productService.UpdateAsync(id, product);
            if (updated == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
