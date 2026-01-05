using HandMadeCakes.Models;
using HandMadeCakes.Models.Enums;
using HandMadeCakes.Services.Cake;
using HandMadeCakes.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HandMadeCakes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICakeInterface _cakeService;

        public ProductController(IProductService productService, ICakeInterface cakeService)
        {
            _productService = productService;
            _cakeService = cakeService;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
             ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
            return View();
        }



        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
            return View(product);
        }

        // POST: Admin/Product/Edit/5
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

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product, IFormFile foto, List<IFormFile> extraImages, string? CakeFlavor, string? CakeDescription)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Enum.GetValues(typeof(ProductCategory));
                return View(product);
            }

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // cria a pasta caso não exista
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // imagem principal
            if (foto != null && foto.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(foto.FileName);
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                product.Cover = "/images/" + fileName;
            }

            // imagens adicionais
            if (extraImages != null && extraImages.Count > 0)
            {
                product.Images = new List<ProductImage>();

                foreach (var image in extraImages)
                {
                    if (image.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(folder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        product.Images.Add(new ProductImage
                        {
                            ImagePath = "/images/" + fileName
                        });
                    }
                }
            }

            // salva no banco
            await _productService.CreateAsync(product);


            // se for Cake, cria CakeModel automaticamente
            if (product.Category == ProductCategory.Cake)
            {
                var dto = new CakeCreateDto
                {
                    ProductId = product.Id,
                    Flavor = CakeFlavor ?? product.Name,
                    Description = CakeDescription ?? product.Description,
                    Price = product.Price
                };

                // coverFoto vem do upload, pode ser null
                await _cakeService.CriarCake(dto, foto, null);
            }

            return RedirectToAction(nameof(Index));


        }
    }
}