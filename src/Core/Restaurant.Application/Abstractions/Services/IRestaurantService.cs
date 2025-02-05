
using Restaurant.Application.ViewModels;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantVM>> GetAllRestaurantsAsync();
        Task<IEnumerable<RestaurantBranchVM>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId, DateTime date);
        Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId);
    }
}
