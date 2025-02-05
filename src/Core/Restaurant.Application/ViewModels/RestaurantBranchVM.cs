

namespace Restaurant.Application.ViewModels
{
    public class RestaurantBranchVM
    {
        
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
    }
}
