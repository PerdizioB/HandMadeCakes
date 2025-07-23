using HandMadeCakes.Data;
using HandMadeCakes.Models;
using HandMadeCakes.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _webhookSecret;// = "whsec_tE6ERW1Ge272zFKzxWLA96hqUpju50xz";
    private readonly AppDbContext _context;
    private readonly IOrderService _orderService;

    public PaymentController(IConfiguration configuration, AppDbContext context, IOrderService orderService)
    {
        _configuration = configuration;
        _context = context;
        _orderService = orderService;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        _webhookSecret = _configuration["Stripe:WebhookSecret"];
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
            Metadata = new Dictionary<string, string>
        {
            { "OrderId", request.OrderId.ToString() }
        }
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
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        // Lê o cabeçalho de assinatura do Stripe
        var stripeSignature = Request.Headers["Stripe-Signature"].FirstOrDefault();

        // Permite ler o corpo várias vezes
        HttpContext.Request.EnableBuffering();

        // Lê o corpo da requisição
        using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
        var json = await reader.ReadToEndAsync();

        // Reposiciona o stream para o início
        Request.Body.Position = 0;

        Event stripeEvent;

        // ======= COMENTADO PARA TESTE LOCAL =========
        // Quando for para PRODUÇÃO, descomente esse bloco:
        /*
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignature,
                _webhookSecret // sua chave do dashboard Stripe
            );
        }
        catch (StripeException e)
        {
            Console.WriteLine($"⚠️ Assinatura inválida: {e.Message}");
            return BadRequest();
        }
        */

        // ======= USADO APENAS PARA TESTE LOCAL =========
        // Cria o evento manualmente (sem validação de assinatura)
        stripeEvent = JsonConvert.DeserializeObject<Event>(json);

        // Processa evento de pagamento com sucesso
        if (stripeEvent.Type == "payment_intent.succeeded")
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (paymentIntent != null && paymentIntent.Metadata.TryGetValue("OrderId", out var orderIdStr)
                && int.TryParse(orderIdStr, out int orderId))
            {
                await _orderService.MarkAsPaidAsync(orderId);
                Console.WriteLine($"✅ Pedido {orderId} marcado como pago.");
            }
            else
            {
                Console.WriteLine("⚠️ OrderId inválido ou ausente no metadata.");
            }
        }

        return Ok();
    }

}