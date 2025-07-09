namespace HandMadeCakes.Models
{
    public class PaymentRequest
    {
        public int Amount { get; set; } // em centavos, por exemplo 1000 = £10.00
        public string Currency { get; set; } = "gbp"; // padrão libra esterlina
    }

}