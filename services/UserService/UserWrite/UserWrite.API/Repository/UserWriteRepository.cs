using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;
using UserWrite.API.Data;
using UserWrite.API.Repository.Interfaces;
using UserWrite.API.Services.Interfaces;

namespace UserWrite.API.Repository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly UserDBContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public UserWriteRepository(UserDBContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<object> DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            bool profileDeleted = false;

            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                profileDeleted = true;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            //My anonymos obj with debug info
            return new
            {
                Message = "User and profile deleted successfully.",
                UserId = userId,
                ProfileDeleted = profileDeleted
            };
        }


        public async Task<UserLoginResultDto> LoginAsync(UserLoginDto login)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
                throw new Exception("User not found.");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Password does not match our records for the provided email.");

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email);

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
