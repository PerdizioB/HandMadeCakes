using HandMadeCakes.ViewModels;
using System.Threading.Tasks;

namespace HandMadeCakes.Services
{
    public interface ICheckoutService
    {
        Task<bool> ProcessOrderAsync(CheckoutViewModel checkout);
    }
}