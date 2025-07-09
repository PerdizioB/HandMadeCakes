using System.ComponentModel.DataAnnotations;

namespace HandMadeCakes.Models
{
    public class CartItem
    {

            public int CakeId { get; set; }
            public string Flavor { get; set; } = "";
            public double Price { get; set; }
            public int Quantity { get; set; } = 1;
        }
    }