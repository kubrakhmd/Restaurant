
using System.ComponentModel.DataAnnotations;
using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.OrderDto
{
    public class UpdateStatus
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Range(0, 2)]
        public OrderStatus Status { get; set; }
    }
}
