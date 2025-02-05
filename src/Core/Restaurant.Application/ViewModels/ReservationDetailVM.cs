using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels
{
           public class ReservationDetailVM
    {
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public DateTime ReservationDate { get; set; }
        public string MealType { get; set; }
        public string TableStatus { get; set; }
        public string ReservationStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
