using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels
{
         public  class ReservationVM
    {
        public string? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string EmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public required int TimeSlotId { get; set; }
        public required DateTime ReservationDate { get; set; }
        public string ReservationStatus { get; set; }
    }
}
