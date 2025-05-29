using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;
using UserWrite.API.Data;
using UserWrite.API.Repository.Interfaces;

namespace UserWrite.API.Repository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly UserDBContext _context;

        public UserWriteRepository(UserDBContext context)
        {
            _context = context;
        }

        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLoginResultDto> LoginAsync(UserLoginDto login)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
                throw new Exception("Invalid credentials.");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Password does not match our records for the provided email.");

            var token = "mock-jwt-token";

            return new UserLoginResultDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
        }


        public async Task<User> RegisterUserAsync(RegisterUserDto userRegistration)
        {
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == userRegistration.Email);

            if (existingUser != null)
                throw new Exception("Email is already registered."); // ill want to rid that from a cache. for faster results.

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task RequestPasswordResetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(PasswordResetDto reset)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponseDto> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
