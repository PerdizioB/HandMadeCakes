using HandMadeCakes.Services;
using HandMadeCakes.Services.Checkout;
using HandMadeCakes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HandMadeCakes.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _checkoutService.ProcessOrderAsync(model);
            if (success)
                return RedirectToAction("Confirmation");

            ModelState.AddModelError("", "Order failed. Try again.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment([FromBody] PaymentConfirmationViewModel model)
        {
            // Exemplo de model:
            // public class PaymentConfirmationViewModel
            // {
            //    public string PaymentIntentId { get; set; }
            //    public int OrderId { get; set; } // opcional
            // }

            if (string.IsNullOrEmpty(model.PaymentIntentId))
                return BadRequest("PaymentIntentId é obrigatório.");

            // Aqui você pode chamar o Stripe API para verificar o status do PaymentIntent
            var service = new Stripe.PaymentIntentService();
            var paymentIntent = await service.GetAsync(model.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                // Atualize seu pedido no banco de dados como pago
                // await _orderService.MarkAsPaid(model.OrderId);

                return Ok(new { success = true });
            }

            return BadRequest(new { success = false, message = "Pagamento não confirmado." });
        }


    }
}