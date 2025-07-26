using HandMadeCakes.Models.Enums;
using HandMadeCakes.Models;
using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.Dto
{
    public class ProductCreateDto
    {
            [Required]
            public string Name { get; set; }

            [Required]
            public ProductCategory Category { get; set; }

            [Required]
            [Range(0.01, double.MaxValue)]
            public decimal Price { get; set; }

            public string Description { get; set; }

            public string Cover { get; set; } // URL img
     }
}

