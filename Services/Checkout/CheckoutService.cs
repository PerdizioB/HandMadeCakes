using HandMadeCakes.Services.Orders;
using HandMadeCakes.Services.Cart;
using HandMadeCakes.ViewModels;
using System.Threading.Tasks;
using HandMadeCakes.Services.Orders;

namespace HandMadeCakes.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public CheckoutService(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        public async Task<int?> ProcessOrderAsync(CheckoutViewModel checkout)
        {
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
                return null; // Carrinho vazio

            var orderId = await _orderService.CreateOrderAsync(checkout, cartItems);
            _cartService.ClearCart();

            return orderId;
        }
    }
}
