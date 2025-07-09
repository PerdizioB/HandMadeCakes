namespace HandMadeCakes.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; } = "Pending";

        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string? UserId { get; set; }

        public ApplicationUser User { get; set; }



    }
}
