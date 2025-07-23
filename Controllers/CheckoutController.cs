using HandMadeCakes.Services;
using HandMadeCakes.Services.Order;
using HandMadeCakes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HandMadeCakes.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IOrderService _orderService;


        public CheckoutController(ICheckoutService checkoutService, IOrderService orderService)
        {
            _checkoutService = checkoutService;
            _orderService = orderService;
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


        public IActionResult Confirmation()
        {
            return View();
        }

    }
}