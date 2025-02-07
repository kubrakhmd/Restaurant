
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.ViewModels;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Implementations.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _dbContext;
        public ReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateOrUpdateReservationAsync(ReservationVM reservation)
        {
            var mealType = _dbContext.TimeSlots.First(f => f.Id == reservation.TimeSlotId).MealType;
            // Define the time for each meal type
            TimeSpan breakfastTime = new TimeSpan(8, 0, 0); // 8:00 AM
            TimeSpan lunchTime = new TimeSpan(12, 0, 0);   // 12:00 PM
            TimeSpan dinnerTime = new TimeSpan(18, 0, 0);  // 6:00 PM

            // Based on the meal type, set the time for ReservationDate
            TimeSpan reservationTime;

            if (mealType == "Breakfast")
            {
                reservationTime = breakfastTime;
            }
            else if (mealType == "Lunch")
            {
                reservationTime = lunchTime;
            }
            else if (mealType == "Dinner")
            {
                reservationTime = dinnerTime;
            }
            else
            {
                throw new ArgumentException("Invalid meal type.");
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.FirstName == reservation.FirstName
            && u.LastName == reservation.LastName && u.Email == reservation.EmailId);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    FirstName = reservation.FirstName,
                    LastName = reservation.LastName,
                    Email = reservation.EmailId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                existingUser = newUser;
            }
            var timeslotToUpdate = await _dbContext.TimeSlots.FindAsync(reservation.TimeSlotId);
            timeslotToUpdate.TableStatus = "Booked";
            _dbContext.TimeSlots.Update(timeslotToUpdate);

            var newReservation = new Reservation
            {
                UserId = existingUser.Id,
                TimeSlotId = reservation.TimeSlotId,
                ReservationDate = reservation.ReservationDate.Date.Add(reservationTime),
                ReservationStatus = reservation.ReservationStatus
            };

            _dbContext.Reservations.Add(newReservation);
            await _dbContext.SaveChangesAsync();

            return newReservation.Id;
        }
        public async Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId)
        {
            var timeSlot = await _dbContext.TimeSlots.FindAsync(timeSlotId);
            return timeSlot;
        }

        public async Task<DiningTableWithTimeSlotVM> UpdateReservationAsync(DiningTableWithTimeSlotVM reservation)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == reservation.UserEmailId);

            var timeslotToUpdate = await _dbContext.TimeSlots.FindAsync(reservation.TimeSlotId);
            timeslotToUpdate.TableStatus = "Checked In";

            _dbContext.TimeSlots.Update(timeslotToUpdate);

            var reservationToUpdate = await _dbContext.Reservations.FirstAsync(f => f.TimeSlotId == reservation.TimeSlotId);
            reservationToUpdate.ReservationStatus = "Checked In";

            _dbContext.Reservations.Update(reservationToUpdate);
            await _dbContext.SaveChangesAsync();

            return reservation;
        }

        public async Task<List<ReservationDetailVM>> GetReservationDetailsAsync()
        {

            var reservationDetails = await _dbContext.Reservations
    .Include(r => r.DiningTable)
        .ThenInclude(dt => dt.RestaurantBranch)
            .ThenInclude(rb => rb.Restaurant)
    .Include(r => r.TimeSlot)
    .Include(r => r.User)
    .Select(r => new ReservationDetailVM
    {
        Name = r.DiningTable.RestaurantBranch.Restaurant.Name,
        BranchName = r.DiningTable.RestaurantBranch.Name, 
        Address = r.DiningTable.RestaurantBranch.Address,
        Phone = r.DiningTable.RestaurantBranch.Phone,
        TableName = r.DiningTable.TableName,
        Capacity = r.DiningTable.Capacity,
        ReservationDate = r.ReservationDate,
        MealType = r.TimeSlot.MealType,
        TableStatus = r.TimeSlot.TableStatus,
        ReservationStatus = r.ReservationStatus,
        FirstName = r.User.FirstName,
        LastName = r.User.LastName,
        Email = r.User.Email
    })
    .ToListAsync();
            return reservationDetails;
        }
    }
}
