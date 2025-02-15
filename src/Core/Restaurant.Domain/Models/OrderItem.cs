
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Restaurant.Domain.Models
{
    public class OrderItem : BaseEntity
    {
   
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }


       public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(MenuItemId))]
        public Menu MenuItem { get; set; }
    }
}
