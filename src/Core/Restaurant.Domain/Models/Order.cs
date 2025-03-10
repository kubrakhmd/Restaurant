﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Models
{

    public class Order : BaseEntity
    {
        [Required]

        public DateTime OrderDate { get; set; }
        [Required, Range(0.01, 10000)]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;

        public string AppUserId { get; set; } 
        public AppUser AppUser { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}