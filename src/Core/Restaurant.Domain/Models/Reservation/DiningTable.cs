using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class DiningTable
    {
        public int Id { get; set; }

        public int RestaurantBranchId { get; set; }
       


        [MaxLength(100)]
        public string? TableName { get; set; }

        [Required]
        public int Capacity { get; set; }

     
        public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();

       
        public RestaurantBranch RestaurantBranch { get; set; } // Navigation Property

        public List <Reservation> Reservations { get; set; }

    }
}
