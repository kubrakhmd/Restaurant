
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace Restaurant.Domain.Models
{
    public class User : IdentityUser
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


    }

    public class Person
    {
        [Key]
        public string Id { get; set; } = "";

        [StringLength(500)]
        public string? CoverImageUrl { get; set; } 

        [ForeignKey(nameof(Id))]
        public User? ApplicationUser { get; set; }

    }


}

