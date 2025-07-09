using Microsoft.AspNetCore.Mvc;
using Stripe;
using HandMadeCakes.Models; // seu modelo PaymentRequest

namespace HandMadeCakes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("pay")]
        public IActionResult Pay([FromBody] PaymentRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethodTypes = new List<string> { "card" },
                Description = "Pagamento do pedido de bolo",
            };

            var service = new PaymentIntentService();

            try
            {
                var paymentIntent = service.Create(options);

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.StripeError.Message });
            }
        }
    }
}
