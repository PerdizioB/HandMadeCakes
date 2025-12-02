
using Microsoft.AspNetCore.Mvc;

namespace HandMadeCakes.Models
{
    public class CakeModel
    {
        public int Id { get; set; }
        public string Flavor { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? Category { get; set; }  // Candy, Rolls, Cakes
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public List<CakeImage> Images { get; set; } = new List<CakeImage>();


    }

}