using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.ViewModels;

using Restaurant.Persistence.Implementations.Repositories;
using Restaurant.Persistence.Implementations.Services;

namespace LSC.RestaurantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        private readonly IReservationService reservationService;
        private readonly IEmailNotification emailNotification;

        public RestaurantController(RestaurantService restaurantService, IReservationService reservationService,
            IEmailNotification emailNotification)
        {
            _restaurantService = restaurantService;
            this.reservationService = reservationService;
            this.emailNotification = emailNotification;
        }

        [HttpGet("restaurants")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantVM>))]

        public async Task<ActionResult<IEnumerable<RestaurantVM>>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("branches/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantBranchVM>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RestaurantBranchVM>>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _restaurantService.GetRestaurantBranchsByRestaurantIdAsync(restaurantId);
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet("diningtables/{branchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotVM>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotVM>>> GetDiningTablesByBranchAsync(int branchId)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId);
            if (diningTables == null)
            {
                return NotFound();
            }
            return Ok(diningTables);
        }

        [HttpGet("diningtables/{branchId}/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotVM>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotVM>>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId, date);
            if (diningTables == null)
            {
                return NotFound();
            }
            return Ok(diningTables);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ReservationVM))]
        [ProducesResponseType(400)]
       
        [AllowAnonymous]
        public async Task<ActionResult<ReservationVM>> CreateReservationAsync(ReservationVM reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the selected time slot exists
            var timeSlot = await reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
            if (!timeSlot)
            {
                return NotFound("Selected time slot not found.");
            }

            // Create a new reservation
            var newReservation = new ReservationVM            {
                UserId = reservation.UserId,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                EmailId = reservation.EmailId,
                PhoneNumber = reservation.PhoneNumber,
                TimeSlotId = reservation.TimeSlotId,
                ReservationDate = reservation.ReservationDate,
                ReservationStatus = reservation.ReservationStatus
            };

            var createdReservation = await reservationService.CreateOrUpdateReservationAsync(newReservation);
            await emailNotification.SendBookingEmailAsync(reservation);

            return new CreatedResult("GetReservation", new { id = createdReservation });
        }

        [HttpGet("getreservations")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReservationDetailVM>))]
        [ProducesResponseType(404)]
   
        public async Task<ActionResult<IEnumerable<ReservationDetailVM>>> GetReservationDetails(int branchId, DateTime date)
        {
            var reservations = await reservationService.GetReservationDetails();

            return Ok(reservations);
        }

    }
}