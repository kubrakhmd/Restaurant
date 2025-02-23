using AutoMapper;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using Restaurant.Application.DTOs.ReservationDto;
using Restaurant.Domain.Models;

namespace Restaurant.Persistence.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public ReservationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task GetAllReservations()
        {
            try
            {
                var reservations = await _context.Rezervations
                    .Include(r => r.User)
                    .ToListAsync();

                if (reservations == null || !reservations.Any())
                {
                  throw new Exception("Rezervation not found");
                }
                var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
              
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task GetReservationsByUserId(int userId)
        {
            try
            {
                var reservations = await _context.Rezervations
                    .Where(r => r.UserId == userId)
                    .Include(r => r.User)
                    .ToListAsync();


                if (reservations == null || !reservations.Any())
                {
                   throw new Exception("Rezervation not found");    
                }
                var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);

               
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task GetReservationById(int id)
        {
            try
            {
                var reservation = await _context.Rezervations
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reservation == null)
                {
                  throw new Exception("Rezervation not found");
                }

                var reservationDto = _mapper.Map<ReservationDto>(reservation);
                
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task CreateReservation(CreateReservationDto CreateR)
        {
            if (CreateR == null)
            {
                throw new Exception("Data is required");
            }

            if (CreateR.NumberOfGuests <= 0 || CreateR.NumberOfGuests > 10)
            {
                     throw new Exception("Number of guests should be between 1 and 10.");
            }

            var openingTime = new TimeSpan(8, 0, 0);
            var closingTime = new TimeSpan(24, 0, 0);
            var reservationTime = CreateR.ReservationDate.TimeOfDay;

            if (reservationTime < openingTime || reservationTime >= closingTime)
            {
                throw new Exception("Reservation time should be between 8:00 AM and 12:00 AM.");
            }

            try
            {
                var existingReservation = await _context.Rezervations
                    .Where(r => r.RestaurantTableId == CreateR.TableId &&
                                r.ReservationDate.Date == CreateR.ReservationDate.Date &&
                                r.ReservationDate.Hour == CreateR.ReservationDate.Hour)
                    .FirstOrDefaultAsync();

                if (existingReservation != null)
                {
                    throw new Exception("Table is already reserved at this time.");
                }

                var reservation = _mapper.Map<Rezervation>(CreateR);
                _context.Rezervations.Add(reservation);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public async Task UpdateReservation(int id, UpdateReservationDto UpdateR)
        {
            try
            {
                var reservation = await _context.Rezervations.FindAsync(id);
                if (reservation == null)
                {
                   throw new Exception("Rezervation not found");
                }

                if (UpdateR.NumberOfGuests <= 0 || UpdateR.NumberOfGuests > 10)
                {
                    throw new Exception("Number of guests should be between 1 and 10.");
                }

                _mapper.Map(UpdateR, reservation);
                reservation.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString()); ;
            }
        }
        public async Task DeleteReservation(int id)
        {
            try
            {
                var reservation = await _context.Rezervations.FindAsync(id);
                if (reservation == null)
                {
                    throw new Exception("Rezervation not found");
                }

                _context.Rezervations.Remove(reservation);
                await _context.SaveChangesAsync();

            
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
