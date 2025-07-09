using HandMadeCakes.Extensions;

using HandMadeCakes.Models;
using Microsoft.AspNetCore.Http;

namespace HandMadeCakes.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "cart";
       

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetCartItemCount()
        {
            var session = _httpContextAccessor.HttpContext!.Session;

            var cart = session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            return cart.Sum(item => item.Quantity);
        }

        public void AddToCart(CartItem item)
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var cart = session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();

            var existingItem = cart.Find(c => c.CakeId == item.CakeId);
            if (existingItem != null)
                existingItem.Quantity += item.Quantity;
            else
                cart.Add(item);

            session.SetObjectAsJson(SessionKey, cart);
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            return session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
        }

        public void RemoveFromCart(int id)
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var cart = session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();

            cart.RemoveAll(c => c.CakeId == id);
            session.SetObjectAsJson(SessionKey, cart);
        }

        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            session.Remove(SessionKey);
        }
    }
}