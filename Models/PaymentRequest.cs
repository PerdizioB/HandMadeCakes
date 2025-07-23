namespace HandMadeCakes.Models
{
    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public int OrderId { get; set; }  

    }

}