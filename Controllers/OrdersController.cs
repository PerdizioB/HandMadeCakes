using HandMadeCakes.Data;
using HandMadeCakes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeCakes.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _orderRepository.GetOrdersByUserIdAsync(user.Id);
            return View(orders);
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (order.UserId != user.Id && !User.IsInRole("Admin"))
                return Forbid();

            return View(order);
        }
    }
}