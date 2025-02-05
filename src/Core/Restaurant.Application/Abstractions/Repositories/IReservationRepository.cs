
using Restaurant.Application.ViewModels;
using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Repositories
{
    public interface IReservationRepository
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationVM reservation);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);

        Task<DiningTableWithTimeSlotVM> UpdateReservationAsync(DiningTableWithTimeSlotVM reservation);
        Task<List<ReservationDetailVM>> GetReservationDetailsAsync();
    }
}
