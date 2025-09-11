using HandMadeCakes.Models;
using HandMadeCakes.Services;

using HandMadeCakes.Services.Cart;
using HandMadeCakes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HandMadeCakes.Services.Orders;


namespace HandMadeCakes.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public CheckoutController(
            ICheckoutService checkoutService,
            IOrderService orderService,
            ICartService cartService)
        {
            _checkoutService = checkoutService;
            _orderService = orderService;
            _cartService = cartService;
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

            var orderId = await _checkoutService.ProcessOrderAsync(model);

            if (orderId.HasValue)
            {
                return RedirectToAction("Payment", new { orderId = orderId.Value });
            }

            ModelState.AddModelError("", "Order failed. Try again.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Payment(int orderId)
        {
            var model = new PaymentViewModel { OrderId = orderId };
            return View(model);
        }

        // GET: /Checkout/GuestInfo
        [HttpGet]
        public IActionResult GuestInfo()
        {
            return View();
        }

        // POST: /Checkout/GuestInfo
        [HttpPost]
        public async Task<IActionResult> GuestInfo(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order); // retorna o form com erros de validação
            }

            // Aqui você deve obter os itens do carrinho (cartItems) para criar o pedido
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(order);
            }

            var savedOrder = await _orderService.CreateGuestOrderAsync(order, cartItems);

            if (savedOrder != null)
            {
                return RedirectToAction("Payment", new { orderId = savedOrder.Id });
            }

            ModelState.AddModelError("", "Could not process your order. Please try again.");
            return View(order);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        [HttpGet]
    
        public IActionResult Choose()
        {
            return View("ChooseCheckout");
        }


    }
}
