namespace HandMadeCakes.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        // FK para o bolo
        public int CakeId { get; set; }
        public CakeModel Cake { get; set; }  // Navegação para o bolo

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // FK para o pedido
        public int OrderId { get; set; }
        public Order Order { get; set; }    // Navegação para o pedido
    }
}
