using HandMadeCakes.Services;
using HandMadeCakes.Services.Cart;
using HandMadeCakes.Services.Order;
using HandMadeCakes.ViewModels;

public class CheckoutService : ICheckoutService
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;

    public CheckoutService(IOrderService orderService, ICartService cartService)
    {
        _orderService = orderService;
        _cartService = cartService;
    }

    public async Task<bool> ProcessOrderAsync(CheckoutViewModel checkout)
    {
        var cartItems = _cartService.GetCartItems();

        if (cartItems == null || !cartItems.Any())
            return false; // Carrinho vazio

        await _orderService.CreateOrderAsync(checkout, cartItems);
        _cartService.ClearCart();

        return true;
    }
}
