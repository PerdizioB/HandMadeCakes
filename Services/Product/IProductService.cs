using HandMadeCakes.Models;
using HandMadeCakes.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Product
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(int id);
        Task<List<ProductModel>> GetByCategoryAsync(ProductCategory category); // ⬅️ atualizado para enum
        Task<ProductModel> CreateAsync(ProductModel product);
        Task<ProductModel> UpdateAsync(int id, ProductModel product);
        Task<bool> DeleteAsync(int id);

        Task<List<ProductModel>> GetAllActiveAsync();

       // Task<bool>  GetAllActiveAsync();

    }
}
