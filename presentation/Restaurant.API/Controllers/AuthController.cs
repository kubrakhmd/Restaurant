
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs.AccountDto;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
   await _authService.Register(registerDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto) { 
              await _authService.Login(loginDto);
            return Ok();
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
           await _authService.ForgetPassword(forgetPasswordDto);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
        await _authService.ResetPassword(resetPasswordDto);
            return Ok();
        }


        //[Authorize]
        [HttpGet("get-users")]
        public async Task<ActionResult> GetUsers()
        {
             await _authService.GetUsers();
            return Ok();
        }
        //[Authorize]
        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
        await _authService.GetUserById(id);
            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("change-role")]
        public async Task<ActionResult> ChangeUserRole(ChangeUserRole changeRoleDto)
        {
         await _authService.ChangeUserRole(changeRoleDto);
            return Ok();
        }

        //[Authorize]
        [HttpGet("get-user-by-username/{username}")]
        public async Task<ActionResult> GetUserByUsername(string username)
        {
          await _authService.FindUserByUsername(username);
            return Ok();
        }
        //[Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            await _authService.DeleteUser(userId);
            return Ok();
        }
    }
}

