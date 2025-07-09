using HandMadeCakes.Models;
using Microsoft.EntityFrameworkCore;

namespace HandMadeCakes.Data
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task UpdateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);


    }
}
