

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models
{
    public class Food:BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }

        public int CategoryId{ get; set; }
        public Category? Category { get; set; }

      public OrderItem? OrderItem { get; set; }
        public string? CoverImageUrl { get; set; }

      

    }
}
