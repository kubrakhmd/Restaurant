

namespace Restaurant.Domain.Models
{
    public class Menu : BaseEntity
    {


        public QR QR { get; set; }
        public List<Food> Foods { get; set; }
    }
}