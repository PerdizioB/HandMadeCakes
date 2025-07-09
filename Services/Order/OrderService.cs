using HandMadeCakes.Data;  
using HandMadeCakes.Dto;    
using HandMadeCakes.Models; 
using HandMadeCakes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IOrderService
{
    void CreateOrder(CheckoutViewModel checkoutData, IEnumerable<CartItem> cartItems);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public void CreateOrder(CheckoutViewModel checkoutData, IEnumerable<CartItem> cartItems)
    {
        // Cria a entidade pedido
        var order = new Order
        {
            FullName = checkoutData.Name,
            Address = checkoutData.Address,
            Phone = checkoutData.Phone,
            Email = checkoutData.Email,
            OrderDate = DateTime.Now,
            OrderItems = cartItems.Select(item => new OrderItem
            {
                CakeId = item.CakeId,
                Quantity = item.Quantity,
                Price = (decimal)item.Price
            }).ToList()
        };

        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}