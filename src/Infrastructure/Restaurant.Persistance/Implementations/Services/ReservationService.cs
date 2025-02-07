
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.ViewModels;

namespace Restaurant.Persistence.Implementations.Services
{

    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
    public ReservationService(IReservationRepository reservationRepository)
    {
        this.reservationRepository = reservationRepository;
    }
    public Task<DiningTableWithTimeSlotVM> CheckInReservationAsync(DiningTableWithTimeSlotVM reservation)
    {
        return reservationRepository.UpdateReservationAsync(reservation);
    }

    public Task<int> CreateOrUpdateReservationAsync(ReservationVM reservation)
    {
        return reservationRepository.CreateOrUpdateReservationAsync(reservation);
    }

    public Task<List<ReservationDetailVM>> GetReservationDetails()
    {
        return reservationRepository.GetReservationDetailsAsync();
    }

    public async Task<bool> TimeSlotIdExistAsync(int timeSlotId)
    {
        var timeSlot = await reservationRepository.GetTimeSlotByIdAsync(timeSlotId);
 
        return timeSlot != null;
    }
}
     
}
