using HandMadeCakes.Models;
using HandMadeCakes.ViewModels;

namespace HandMadeCakes.Services.Order
{
    public interface IOrderService
    {
        void CreateOrder(CheckoutViewModel checkoutData, IEnumerable<CartItem> cartItems);
    }
}
