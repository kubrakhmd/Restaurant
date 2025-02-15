
using Restaurant.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOs.AccountDto
{
    public class ChangeUserRole
    {
        [Required]
        public int UserId { get; set; }
  
        public UserRole NewRole { get; set; }
    }
}
