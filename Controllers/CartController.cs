using HandMadeCakes.Data;
using HandMadeCakes.Models;
using HandMadeCakes.Services.Cart;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeCakes.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Add(int id, string name, double price)
        {
            var item = new CartItem
            {
                CakeId = id,
                Flavor = name,
                Price = price,
                Quantity = 1
            };

            _cartService.AddToCart(item);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}