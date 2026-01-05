using HandMadeCakes.Dto;
using HandMadeCakes.Models;

namespace HandMadeCakes.Services.Cake
{
    public interface ICakeInterface
    {

        Task<List<CakeModel>> GetCakes();
        Task<CakeModel> GetCakePorId(int id);
        Task<CakeModel> CriarCake(CakeCreateDto CakeCreateDto, IFormFile? coverFoto, List<IFormFile> fotos);
        Task<CakeModel> EditarCake(CakeModel cake, IFormFile? foto);
        Task<CakeModel> Edit(CakeModel Cake, IFormFile? coverFoto, List<IFormFile>? fotos);

        Task<CakeModel> RemoverCake(int id);
        Task<List<CakeModel>> GetCakesFiltro(string? pesquisar);
    }
}
