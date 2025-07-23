using HandMadeCakes.Data;
using HandMadeCakes.Models;
using HandMadeCakes.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(CheckoutViewModel checkoutData, List<CartItem> cartItems)
        {
            var order = new HandMadeCakes.Models.Order
            {
                FullName = checkoutData.FullName,
                Address = checkoutData.Address,
                Phone = checkoutData.Phone,
                Email = checkoutData.Email,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    CakeId = item.CakeId,
                    Quantity = item.Quantity,
                    Price = (decimal)item.Price
                }).ToList(),
                TotalAmount = (decimal)cartItems.Sum(i => i.Price * i.Quantity),
                IsPaid = false // importante!
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id; // retorna o ID do pedido gerado
        }

        public async Task MarkAsPaidAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Pedido não encontrado.");

            order.IsPaid = true;
            await _context.SaveChangesAsync();
        }
    }
}
