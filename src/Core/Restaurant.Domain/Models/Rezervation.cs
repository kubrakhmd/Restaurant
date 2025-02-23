using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
  public class Rezervation:BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RestaurantTableId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [StringLength(250)]
        public string? SpecialRequest { get; set; }

 
        public User? User { get; set; }


        public RestaurantTable? RestaurantTable { get; set; }
    }
}
