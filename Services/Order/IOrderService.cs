using HandMadeCakes.Models;
using HandMadeCakes.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Orders
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CheckoutViewModel checkoutData, List<CartItem> cartItems);

        Task<Order> CreateGuestOrderAsync(Order order, List<CartItem> cartItems);

        Task MarkAsPaidAsync(int orderId);
    }
}
