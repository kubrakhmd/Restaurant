
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Restaurant.Domain.Models
{
    public class OrderItem : BaseEntity
    {
   
        [Required]
        public int FoodId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }


       public int OrderId { get; set; }
        public Order Order { get; set; }

        
        public Food Food { get; set; }
    }
}
