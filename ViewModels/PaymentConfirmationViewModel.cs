namespace HandMadeCakes.ViewModels
{
    public class PaymentConfirmationViewModel
    {
        public string PaymentIntentId { get; set; }
        public int? OrderId { get; set; }  // opcional, se quiser passar o Id do pedido
    }
}
