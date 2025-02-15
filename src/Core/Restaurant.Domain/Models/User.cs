
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Enums;
namespace Restaurant.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public long IdNumber { get; set; }
        public string Name { get; set; } = "";
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; }
        public string Address { get; set; } = "";
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public byte Status { get; set; }
        [NotMapped]
        public string? Password { get; set; }

        [NotMapped]  
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

        public bool IsActive { get; set; } = true; 
        public List<OrderItem>Orders { get; set; }

    }

    public class    User
    {
        
        public int Id { get; set; }

        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required, StringLength(20)]
        public UserRole Role { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry
        { get; set; }


        public  AppUser? ApplicationUser { get; set; }

    }


}

