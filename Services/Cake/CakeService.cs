using HandMadeCakes.Data;
using HandMadeCakes.Dto;
using HandMadeCakes.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HandMadeCakes.Services.Cake
{
    public class CakeService : ICakeInterface
    {
        private readonly AppDbContext _context;
        private readonly string _sistema;

        public CakeService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }

        // Gera caminho único para imagem
        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeArquivo = Path.GetFileNameWithoutExtension(foto.FileName)
                                .Replace(" ", "")
                                .ToLower() + "_" + codigoUnico + Path.GetExtension(foto.FileName);

            var caminhoPasta = Path.Combine(_sistema, "image");

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                foto.CopyTo(stream);
            }

            return nomeArquivo;
        }

        // Cria Cake
        public async Task<CakeModel> CriarCake(CakeCreateDto cakeCreateDto, IFormFile? coverFoto, List<IFormFile>? fotos)
        {
            // Buscar Product
            var product = await _context.Product.FindAsync(cakeCreateDto.ProductId);
            if (product == null)
                throw new Exception("Product not found. Cake cannot be created.");

            // Determinar cover (PADRONIZADO COM PRODUCT)
            string coverPath;

            if (!string.IsNullOrEmpty(product.Cover))
            {
                // Usa exatamente a mesma imagem do Product
                coverPath = product.Cover;
            }
            else
            {
                // fallback
                coverPath = "/images/default-cake.png";
            }


            // Criar CakeModel
            var cake = new CakeModel
            {
                Flavor = !string.IsNullOrEmpty(cakeCreateDto.Flavor) ? cakeCreateDto.Flavor : product.Name ?? "Unknown",
                Description = !string.IsNullOrEmpty(cakeCreateDto.Description) ? cakeCreateDto.Description : product.Description ?? "",
                Price = cakeCreateDto.Price != 0 ? cakeCreateDto.Price : product.Price,
                Cover = coverPath,
                ProductId = cakeCreateDto.ProductId,
                Images = new List<CakeImage>()
            };

            // Salvar Cake
            try
            {
                _context.Cake.Add(cake);
                await _context.SaveChangesAsync(); // Gera Id
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save Cake: " + ex.Message);
            }

            // Salvar imagens extras
            if (fotos != null && fotos.Any())
            {
                foreach (var foto in fotos)
                {
                    var path = GeraCaminhoArquivo(foto);
                    _context.CakeImages.Add(new CakeImage
                    {
                        CakeId = cake.Id,
                        ImagePath = path
                    });
                }
                await _context.SaveChangesAsync();
            }

            return cake;
        }

        // Buscar todos os Cakes
        public async Task<List<CakeModel>> GetCakes()
        {
            try
            {
                return await _context.Cake
                    .Include(c => c.Product)
                    .Include(c => c.Images)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching cakes: " + ex.Message);
            }
        }

        // Buscar Cake por Id
        public async Task<CakeModel?> GetCakePorId(int id)
        {
            try
            {
                return await _context.Cake
                    .Include(c => c.Images)
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching cake by id: " + ex.Message);
            }
        }

        // Editar Cake
        public async Task<CakeModel> Edit(CakeModel Cake, IFormFile? coverFoto, List<IFormFile>? fotos)
        {
            var cakeBanco = await _context.Cake
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == Cake.Id);

            if (cakeBanco == null)
                throw new Exception("Cake not found.");

            // Atualizar dados
            cakeBanco.Flavor = Cake.Flavor;
            cakeBanco.Description = Cake.Description;
            cakeBanco.Price = Cake.Price;

            // Salvar imagens extras
            if (fotos != null && fotos.Any())
            {
                foreach (var foto in fotos)
                {
                    var path = GeraCaminhoArquivo(foto);
                    _context.CakeImages.Add(new CakeImage
                    {
                        CakeId = cakeBanco.Id,
                        ImagePath = path
                    });
                }
            }

            await _context.SaveChangesAsync();
            return cakeBanco;
        }


        // Remover Cake
        public async Task<CakeModel> RemoverCake(int id)
        {
            var cake = await _context.Cake.FirstOrDefaultAsync(c => c.Id == id);
            if (cake == null) throw new Exception("Cake not found.");

            _context.Cake.Remove(cake);
            await _context.SaveChangesAsync();

            return cake;
        }

        // Filtrar Cakes
        public async Task<List<CakeModel>> GetCakesFiltro(string? pesquisar)
        {
            try
            {
                return await _context.Cake
                    .Include(c => c.Product)
                    .Include(c => c.Images)
                    .Where(c => c.Flavor.Contains(pesquisar ?? string.Empty))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error filtering cakes: " + ex.Message);
            }
        }

        // Métodos não implementados
        public Task<List<CakeModel>> GetCake() => throw new NotImplementedException();
        public Task<List<CakeModel>> GetCakeFiltro(string? pesquisar) => throw new NotImplementedException();
        public Task<CakeModel> EditarCake(CakeModel Cake, IFormFile? foto) => throw new NotImplementedException();
    }
}
