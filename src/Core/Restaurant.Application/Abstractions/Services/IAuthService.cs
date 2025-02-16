
using Restaurant.Application.DTOs.AccountDto;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto registerDto);
        Task Login(LoginDto loginDto);
        Task ForgetPassword(ForgetPasswordDto forgetPasswordDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
        Task GetUsers();
        Task GetUserById(int id);
        Task FindUserByUsername(string username);
        Task ChangeUserRole(ChangeUserRole userRole);
        Task Logout();
        Task  DeleteUser(int userId);
    }
}

 