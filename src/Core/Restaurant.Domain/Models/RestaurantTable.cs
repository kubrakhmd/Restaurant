using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class RestaurantTable: BaseEntity
    {
        [Required]
        public int TableNumber { get; set; }

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }
        public List<Rezervation> Rezervations { get; set; } 
    }
}
