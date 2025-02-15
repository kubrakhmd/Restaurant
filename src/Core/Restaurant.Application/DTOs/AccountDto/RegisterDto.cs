using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOs.AccountDto
{
    public class RegisterDto
    {
      
        public string Username { get; set; }
     
        public string Password { get; set; }
        [EmailAddress]
      
        public string Email { get; set; }
    }
}
