using HandMadeCakes.Models;
using HandMadeCakes.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Order
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CheckoutViewModel checkoutData, IEnumerable<CartItem> cartItems);
    }
}
