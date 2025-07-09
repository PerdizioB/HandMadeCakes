using HandMadeCakes.Data;
using HandMadeCakes.Models;
using HandMadeCakes.Services.Cart;
using HandMadeCakes.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using OrderModel = HandMadeCakes.Models.Order;


namespace HandMadeCakes.Services.Checkout
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;

        public CheckoutService(IOrderRepository orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        public async Task<bool> ProcessOrderAsync(CheckoutViewModel checkout)
        {
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
                return false; // Carrinho vazio

            var order = new OrderModel
            {
                FullName = checkout.Name,
                Email = checkout.Email,
                Address = checkout.Address,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    CakeId = ci.CakeId,
                    Quantity = ci.Quantity,
                    Price = (decimal)ci.Price
                }).ToList(),
                TotalAmount = (decimal)cartItems.Sum(ci => ci.Price * ci.Quantity)
            };

            await _orderRepository.SaveOrderAsync(order);
            _cartService.ClearCart();

            return true;
        }
    }
}