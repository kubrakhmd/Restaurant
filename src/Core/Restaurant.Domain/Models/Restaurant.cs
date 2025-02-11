﻿

namespace Restaurant.Domain.Models
{
     public class Restaurant:BaseEntity
    {
      
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string? PhoneNumber { get; set; }

        public List<Menu>? Menus { get; set; }
    }
}
