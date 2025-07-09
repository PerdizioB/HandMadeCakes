using HandMadeCakes.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HandMadeCakes.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)  // Para carregar os itens do pedido
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders //Acessar BD
     .Include(o => o.OrderItems)
         .ThenInclude(oi => oi.Cake)  // carrega os bolos também
     .FirstOrDefaultAsync(o => o.Id == id);


       
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Cake)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        

    }
}