using HandMadeCakes.Models;
using HandMadeCakes.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Order
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CheckoutViewModel checkoutData, List<CartItem> cartItems);
        Task MarkAsPaidAsync(int orderId);
    }
}
