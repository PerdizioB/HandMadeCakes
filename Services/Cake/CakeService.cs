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

        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeArquivo = Path.GetFileNameWithoutExtension(foto.FileName)
                                .Replace(" ", "")
                                .ToLower() + "_" + codigoUnico + Path.GetExtension(foto.FileName);

            var caminhoPasta = Path.Combine(_sistema, "imagem");

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                foto.CopyTo(stream);
            }

            return nomeArquivo;
        }

        public async Task<CakeModel> CriarCake(CakeCreateDto cakeCreateDto, IFormFile coverFoto, List<IFormFile>? fotos)
        {
            if (coverFoto == null)
                throw new Exception("A imagem principal (capa) é obrigatória.");

            var nomeCaminhoCover = GeraCaminhoArquivo(coverFoto);

            var cake = new CakeModel
            {
                Flavor = cakeCreateDto.Flavor,
                Description = cakeCreateDto.Description,
                Price = cakeCreateDto.Price,
                Cover = nomeCaminhoCover,
                Images = new List<CakeImage>()
            };

            _context.Add(cake);
            await _context.SaveChangesAsync();

            if (fotos != null && fotos.Any())
            {
                foreach (var foto in fotos)
                {
                    var nomeCaminhoImagem = GeraCaminhoArquivo(foto);
                    var cakeImage = new CakeImage
                    {
                        CakeId = cake.Id,
                        ImagePath = nomeCaminhoImagem
                    };
                    _context.CakeImages.Add(cakeImage);
                }
                await _context.SaveChangesAsync();
            }

            return cake;
        }

        public async Task<List<CakeModel>> GetCakes()
        {
            try
            {
                return await _context.Cake.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter bolos: " + ex.Message);
            }
        }

        public async Task<CakeModel?> GetCakePorId(int id)
        {
            try
            {
                return await _context.Cake
                    .Include(c => c.Images)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter bolo por id: " + ex.Message);
            }
        }

        public async Task<CakeModel> Edit(CakeModel Cake, IFormFile? coverFoto, List<IFormFile>? fotos)
        {
            var cakeBanco = await _context.Cake.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id == Cake.Id);
            if (cakeBanco == null) throw new Exception("Bolo não encontrado");

            // Atualiza capa
            if (coverFoto != null)
            {
                var caminhoCoverExistente = Path.Combine(_sistema, "imagem", cakeBanco.Cover);
                if (File.Exists(caminhoCoverExistente))
                    File.Delete(caminhoCoverExistente);

                cakeBanco.Cover = GeraCaminhoArquivo(coverFoto);
            }

            // Atualiza dados básicos
            cakeBanco.Flavor = Cake.Flavor;
            cakeBanco.Description = Cake.Description;
            cakeBanco.Price = Cake.Price;

            // Salva as novas imagens extras, se houver
            if (fotos != null && fotos.Any())
            {
                foreach (var foto in fotos)
                {
                    var nomeImagem = GeraCaminhoArquivo(foto);
                    var cakeImage = new CakeImage
                    {
                        CakeId = cakeBanco.Id,
                        ImagePath = nomeImagem
                    };
                    _context.CakeImages.Add(cakeImage);
                }
            }

            await _context.SaveChangesAsync();

            return cakeBanco;
        }
        public async Task<CakeModel> RemoverCake(int id)
        {
            try
            {
                var cake = await _context.Cake.FirstOrDefaultAsync(c => c.Id == id);
                if (cake == null)
                    throw new Exception("Bolo não encontrado");

                _context.Remove(cake);
                await _context.SaveChangesAsync();

                return cake;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover bolo: " + ex.Message);
            }
        }

        public async Task<List<CakeModel>> GetCakesFiltro(string? pesquisar)
        {
            try
            {
                return await _context.Cake
                    .Where(c => c.Flavor.Contains(pesquisar ?? string.Empty))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar bolos: " + ex.Message);
            }
        }

        // Métodos não implementados podem ficar com NotImplementedException
        public Task<List<CakeModel>> GetCake()
            => throw new NotImplementedException();

        public Task<List<CakeModel>> GetCakeFiltro(string? pesquisar)
            => throw new NotImplementedException();

        public Task<CakeModel> EditarCake(CakeModel Cake, IFormFile? foto)
            => throw new NotImplementedException();
    }

}
