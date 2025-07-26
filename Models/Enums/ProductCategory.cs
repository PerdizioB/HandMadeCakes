using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.Models.Enums
{
    public enum ProductCategory
    {
        [Display(Name = "Cakes")]
        Cake,

        [Display(Name = "Sweets")]
        Sweet,

        [Display(Name = "Savories")]
        Savoury
    }
}
