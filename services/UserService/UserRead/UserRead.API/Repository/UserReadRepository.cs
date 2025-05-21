using Microsoft.EntityFrameworkCore;
using UserRead.API.Data;
using UserRead.API.Repository.Interfaces;
using UserShared.Lib.Models;

namespace UserRead.API.Repository
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly UserDBContext _context;

        public UserReadRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }
    }
}
