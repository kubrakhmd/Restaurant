using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.DTOs.ReservationDto;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IReservationService
    {
        Task GetAllReservations();
        Task GetReservationsByUserId(int userId);
        Task GetReservationById(int id);
        Task CreateReservation(CreateReservationDto CreateR);
        Task UpdateReservation(int id, UpdateReservationDto UpdateR);
        Task DeleteReservation(int id);
    }
}
