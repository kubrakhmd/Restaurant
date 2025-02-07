
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.ViewModels;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Implementations.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _dbContext;


        public RestaurantRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User?> GetUserAsync(string emailId)
        {
            return _dbContext.Users.FirstOrDefaultAsync(f => f.Email.Equals(emailId));
        }

        public async Task<RestaurantReservaitonDetail> GetRestaurantReservationDetailsAsync(int timeSlotId)
        {
            var reservationDetail = await _dbContext.TimeSlots
                .Where(ts => ts.Id == timeSlotId) 
                .Include(ts => ts.DiningTable) 
                    .ThenInclude(dt => dt.RestaurantBranch) 
                        .ThenInclude(rb => rb.Restaurant) 
                .Select(ts => new RestaurantReservaitonDetail
                {
                    RestaurantName = ts.DiningTable.RestaurantBranch.Restaurant.Name,
                    BranchName = ts.DiningTable.RestaurantBranch.Name,
                    Address = ts.DiningTable.RestaurantBranch.Address,
                    TableName = ts.DiningTable.TableName,
                    Capacity = ts.DiningTable.Capacity,
                    MealType = ts.MealType,
                    ReservationDay = ts.ReservationDay
                })
                .FirstOrDefaultAsync();

            return reservationDetail;
        }

        public async Task<IEnumerable<RestaurantVM>> GetAllRestaurantsAsync()
        {
            var restaurants = await _dbContext.Restaurants
                .Select(r => new RestaurantVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    Phone = r.Phone,
                    Email = r.Email,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();

            return restaurants;
        }

      
        public async Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            var diningTables = await _dbContext.DiningTables
             .Where(dt => dt.RestaurantBranchId == branchId)
             .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
             {
                 dt.RestaurantBranchId,
                 dt.TableName,
                 dt.Capacity,
                 ts.ReservationDay,
                 ts.MealType,
                 ts.TableStatus,
                 ts.Id
             })
             .Where(ts => ts.ReservationDay.Date == date.Date)
             .OrderBy(ts => ts.Id)
             .ThenBy(ts => ts.MealType)
             .ToListAsync();

            return diningTables.Select(dt => new DiningTableWithTimeSlotVM
            {
                BranchId = dt.RestaurantBranchId,
                ReservationDay = dt.ReservationDay.Date,
                TableName = dt.TableName,
                Capacity = dt.Capacity,
                MealType = dt.MealType,
                TableStatus = dt.TableStatus,
                TimeSlotId = dt.Id
            });
           
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotVM>> GetDiningTablesByBranchAsync(int branchId)
        {
            var data = await _dbContext.RestaurantBranches
                .Where(rb => rb.Id == branchId) 
                .Include(rb => rb.DiningTables) 
                    .ThenInclude(dt => dt.TimeSlots) 
                .SelectMany(rb => rb.DiningTables, (rb, dt) => new { rb, dt }) 
                .SelectMany(x => x.dt.TimeSlots, (x, ts) => new { x.rb, x.dt, ts }) 
                .Where(x => x.ts.ReservationDay >= DateTime.Now.Date) 
                .OrderBy(x => x.ts.Id) 
                .ThenBy(x => x.ts.MealType) 
                .Select(x => new DiningTableWithTimeSlotVM
                {
                    BranchId = x.rb.Id,
                    Capacity = x.dt.Capacity,
                    TableName = x.dt.TableName,
                    MealType = x.ts.MealType,
                    ReservationDay = x.ts.ReservationDay,
                    TableStatus = x.ts.TableStatus,
                    TimeSlotId = x.ts.Id,
                  
                    UserEmailId = x.ts.Reservations != null && x.ts.Reservations.Any() ?
                        x.ts.Reservations.FirstOrDefault().User.Email.ToLower() : null
                })
                .ToListAsync();

            return data;
        }


        public async Task<IEnumerable<RestaurantBranchVM>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _dbContext.RestaurantBranches
                .Where(rb => rb.RestaurantId == restaurantId)
                .Select(rb => new RestaurantBranchVM
                {
                    Id = rb.Id,
                    RestaurantId = rb.RestaurantId,
                    Name = rb.Name,
                    Address = rb.Address,
                    Phone = rb.Phone,
                    Email = rb.Email,
                    ImageUrl = rb.ImageUrl
                })
                .ToListAsync();

            return branches;
        }





    }

}
