

namespace Restaurant.Domain.Models
{
    public class Menu : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public List<Food> Foods { get; set; }   
    }
}