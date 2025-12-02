using HandMadeCakes.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ProductCategory Category { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public string Cover { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
