
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOs.OrderDto
{
    public class OrderItemDto
    {

        public int FoodId { get; set; }
        [Required, Range(1, 100)]
        public int Quantity { get; set; }
    }
    public class ShowOrderItem
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
