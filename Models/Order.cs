using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        public string? Status { get; set; } = "Pending";

        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();

        public decimal TotalAmount { get; set; }

        public string? UserId { get; set; }

        public bool IsPaid { get; set; } = false;

        public ApplicationUser? User { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
       [Required]
        public string DeliveryTime { get; set; }
    }
}

