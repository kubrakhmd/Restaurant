using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.ViewModels;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IReservationService
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationVM reservation);
        Task<bool> TimeSlotIdExistAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotVM> CheckInReservationAsync(DiningTableWithTimeSlotVM reservation);
        Task<List<ReservationDetailVM>> GetReservationDetails();
    }
}
