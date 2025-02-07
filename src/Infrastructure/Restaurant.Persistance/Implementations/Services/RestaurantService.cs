using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.ViewModels;

namespace Restaurant.Persistence.Implementations.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<RestaurantVM>> GetAllRestaurantsAsync()
        {
            return await _restaurantRepository.GetAllRestaurantsAsync();
        }

        public async Task<IEnumerable<RestaurantBranchVM>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId)
        {
            return await _restaurantRepository.GetRestaurantBranchsByRestaurantIdAsync(restaurantId);
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            return await _restaurantRepository.GetDiningTablesByBranchAsync(branchId, date);
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId)
        {
            return await _restaurantRepository.GetDiningTablesByBranchAsync(branchId);
        }

    }


}
