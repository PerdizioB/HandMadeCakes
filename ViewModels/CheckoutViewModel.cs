using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Shipping Address")]
        public string Address { get; set; }

        // Se quiser, pode adicionar cidade, estado, CEP...
        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Delivery date is required")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }

        [Required(ErrorMessage = "Delivery time is required")]
        public string DeliveryTime { get; set; }

    }
}
