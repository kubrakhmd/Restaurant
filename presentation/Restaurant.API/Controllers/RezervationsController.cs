using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs.ReservationDto;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public RezervationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult> GetReservations()
        {
         await _reservationService.GetAllReservations();
            return Ok();
        }

        // GET: api/Reservations/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetReservationsByUserId(int userId)
        {
             await _reservationService.GetReservationsByUserId(userId);
            return Ok();
        }

        // GET: api/Reservations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReservation(int id)
        {
           await _reservationService.GetReservationById(id);
            return Ok();
        }

        // POST: api/Reservations/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {
            await _reservationService.CreateReservation(reservationDto);
            return Ok();
        }

        // PUT: api/Reservations/update/{id}
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
          await _reservationService.UpdateReservation(id, reservationDto);
            return NoContent();
        }

        // DELETE: api/Reservations/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
        await _reservationService.DeleteReservation(id);
            return NoContent();
        }
    }
}

