using HandMadeCakes.Data;
using HandMadeCakes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HandMadeCakes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Action para listar todos os pedidos
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return View(orders);  // Passa a lista para a View Index.cshtml
        }

        // Action para mostrar os detalhes de um pedido específico
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();  // Se não encontrar, retorna 404
            }
            return View(order);  // Passa o pedido para a View Details.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);  // Você precisa criar esse método no repositório

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
