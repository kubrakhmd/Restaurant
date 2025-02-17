
using System.Text;

using Microsoft.Extensions.Configuration;
using Restaurant.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Restaurant.Persistence.Context;
using AutoMapper;
using Restaurant.Application.DTOs.AccountDto;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Abstractions.Services;

namespace Restaurant.Persistence.Implementations.Services
{
    
        public class AuthService : IAuthService
        {
            private readonly AppDbContext _context;
            private readonly IConfiguration _configuration;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IMapper _mapper;

        public AuthService(AppDbContext context, IConfiguration configuration, SignInManager<AppUser> signInManager, IMapper mapper) { 
                _context = context;
                _configuration = configuration;
                _signInManager = signInManager;
                _mapper = mapper;
            
            }
            public async Task Register(RegisterDto registerDto)
            {
                try
                {
                    if (await _context.Users.AnyAsync(u => u.UserName == registerDto.Username))
                    {
                    throw new Exception("Username already exists");
                    }

                    if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                    {
                      throw new Exception("Email already exists");  
                }

                    var user = new User
                    {
                        UserName = registerDto.Username,
                        PasswordHash = HashPassword(registerDto.Password),
                        Email = registerDto.Email,
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                 
                }
                catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());

            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task Login(LoginDto loginDto)
            {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserOrEmail || u.Email == loginDto.UserOrEmail);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                bool isPasswordValid = VerifyPassword(loginDto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    throw new Exception("Invalid password");
                }

                var userDto = _mapper.Map<UserDto>(user);

                var token = GenerateJwtToken(user);
                userDto.Token = token;


            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception (ex.ToString());
            }
            }
            public async Task ForgetPassword(ForgetPasswordDto forgetPasswordDto)
            {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgetPasswordDto.Email);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var resetToken = GenerateResetToken();
                user.ResetToken = resetToken;
                user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(3);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            }
            public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == resetPasswordDto.ResetToken && u.ResetTokenExpiry > DateTime.UtcNow);
                    if (user == null)
                    {
                     throw new Exception("Invalid token or token expired");
                }

                    user.PasswordHash = HashPassword(resetPasswordDto.NewPassword);
                    user.ResetToken = null;
                    user.ResetTokenExpiry = null;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task GetUsers()
            {
                try
                {
                    var users = await _context.Users.ToListAsync();
                    if (users == null || users.Count == 0)
                    {
                       throw new Exception("No users found");
                }

                    var usersDto = _mapper.Map<List<UserDto>>(users);
                   
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task GetUserById(int id)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        throw new Exception("User not found");
                }

                    var userDto = _mapper.Map<UserDto>(user);
                    
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            }
            public async Task FindUserByUsername(string username)
            {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var userDto = _mapper.Map<UserDto>(user);
                            }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                  throw new Exception(ex.ToString());
            }
            }
            public async Task ChangeUserRole(ChangeUserRole userRole)
            {
                try
                {
                    var user = await _context.Users.FindAsync(userRole.UserId);
                    if (user == null)
                    {
                     throw new Exception("User not found");
                }

                    user.Role = userRole.NewRole;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task Logout()
            {
                try
                {
                    await _signInManager.SignOutAsync();
                  
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task DeleteUser(int userId)
            {
                try
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null)
                    {
                      throw new Exception ("User not found");   
                }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                   
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            private string HashPassword(string password)
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return $"{Convert.ToBase64String(salt)}.{hashed}";
            }
            private bool VerifyPassword(string password, string storedHash)
            {
                var parts = storedHash.Split('.');
                var salt = Convert.FromBase64String(parts[0]);
                var hash = parts[1];

                var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return hash == hashed;
            }
            private string GenerateJwtToken(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _configuration["Jwt:ServerSecret"];

                var key = Encoding.ASCII.GetBytes(secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            private string GenerateResetToken()
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    byte[] tokenData = new byte[32];
                    rng.GetBytes(tokenData);
                    return Convert.ToBase64String(tokenData);
                }
            }
        }
    }


