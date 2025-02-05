
using Restaurant.Application.ViewModels;
using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<RestaurantVM>> GetAllRestaurantsAsync();
        Task<IEnumerable<RestaurantBranchVM>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId, DateTime date);

        Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId);
        Task<RestaurantReservaitonDetail> GetRestaurantReservationDetailsAsync(int timeSlotId);

        Task<User?> GetUserAsync(string emailId);
    }
}
