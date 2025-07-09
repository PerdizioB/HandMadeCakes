using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Shipping Address")]
        public string Address { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }
    }
}