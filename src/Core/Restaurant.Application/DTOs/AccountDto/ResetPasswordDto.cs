using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOs.AccountDto
{
    public class ResetPasswordDto
    {
   
        public string ResetToken { get; set; }
    
        public string NewPassword { get; set; }
    }
}
