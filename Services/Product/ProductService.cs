using HandMadeCakes.Models;
using HandMadeCakes.Models.Enums;
using HandMadeCakes.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {

            return await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProductModel>> GetByCategoryAsync(ProductCategory category)
        {
            return await _context.Product
                .Where(p => p.Category == category)
                .ToListAsync();
        }


        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();



            return product;
        }

        public async Task<ProductModel> UpdateAsync(int id, ProductModel product)
        {
            var existingProduct = await _context.Product.FindAsync(id);
            if (existingProduct == null)
                return null;

            //UPDATE FIELDS

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.Cover = product.Cover; // Atualiza a imagem


            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
                return false;

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<ProductModel>> GetAllActiveAsync()
        {
            return await _context.Product
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        //public async bool IsActive { get; set; }

    }
}
