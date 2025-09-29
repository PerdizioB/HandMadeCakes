using HandMadeCakes.Data;
using HandMadeCakes.Models;
using HandMadeCakes.Services.Orders;
using HandMadeCakes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandMadeCakes.Services.Orders
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
            var order = new Order
            {
                FullName = checkoutData.FullName,
                Address = checkoutData.Address,
                Phone = checkoutData.Phone,
                Email = checkoutData.Email,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                IsPaid = false,
                DeliveryDate = (DateTime)checkoutData.DeliveryDate,  // NOVO
                DeliveryTime = checkoutData.DeliveryTime,  // NOVO
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    CakeId = item.CakeId,
                    Quantity = item.Quantity,
                    Price = (decimal)item.Price
                }).ToList(),
                TotalAmount = (decimal)cartItems.Sum(i => i.Price * i.Quantity)
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<Order> CreateGuestOrderAsync(Order order, List<CartItem> cartItems)
        {
            order.TotalAmount = cartItems.Sum(i => i.Quantity * (decimal)i.Price);
            order.Status = "Pending";
            order.OrderDate = DateTime.UtcNow;
            order.IsPaid = false;

            order.OrderItems = cartItems.Select(item => new OrderItem
            {
                CakeId = item.CakeId,
                Quantity = item.Quantity,
                Price = (decimal)item.Price
            }).ToList();

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
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
