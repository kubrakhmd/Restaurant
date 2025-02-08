using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Domain.Models;
using Restaurant.Application.ViewModels;
using Restaurant.Persistence.Implementations.Repositories;
using Microsoft.Identity.Web.Resource;

namespace Restaurant.API.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
      
        public class ReservationController : ControllerBase
        {
            private readonly IReservationService reservationService;
            private readonly IHttpContextAccessor _contextAccessor;
         
            private readonly IEmailNotification emailNotification;
            private ClaimsPrincipal _currentPrincipal;
            
            private string _currentPrincipalId = string.Empty;

            public ReservationController(IReservationService reservationService, 
                IEmailNotification emailNotification,
                IHttpContextAccessor contextAccessor)
            {
                this.reservationService = reservationService;
                _contextAccessor = contextAccessor;
              
                this.emailNotification = emailNotification;

                _currentPrincipal = GetCurrentClaimsPrincipal();

              
            }


            [HttpGet("{id}", Name = "GetReservation")]
          
            public async Task<ActionResult<Reservation>> GetReservation(int id)
            {
        
                return Ok();
            }


            [HttpPost("CheckIn")]
            [ProducesResponseType(200, Type = typeof(ReservationVM))]
            [ProducesResponseType(400)]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]

        public async Task<ActionResult<ReservationVM>> CheckInReservationAsync(DiningTableWithTimeSlotVM reservation)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var timeSlot = await reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
                if (!timeSlot)
                {
                    return NotFound("Selected time slot not found.");
                }
                var response = await reservationService.CheckInReservationAsync(reservation);
                await emailNotification.SendCheckInEmailAsync(reservation);
                return Ok(response);
            }

            private ClaimsPrincipal GetCurrentClaimsPrincipal()
            {
              
                if (_contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User != null)
                {
                    return _contextAccessor.HttpContext.User;
                }

                return null;
            }

            
         
        }
    }


