using HandMadeCakes.Models;

namespace HandMadeCakes.Services.Cart
{
    public interface ICartService
    {
        void AddToCart(CartItem item);
        List<CartItem> GetCartItems();
        void RemoveFromCart(int id);
        void ClearCart();
        int GetCartItemCount();

    }
}