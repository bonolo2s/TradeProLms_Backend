using Microsoft.EntityFrameworkCore;
using UserRead.API.Data;
using UserRead.API.Repository.Interfaces;
using UserShared.Lib.Models;

namespace UserRead.API.Repository
{
    public class ProfileReadRepository : IProfileReadRepository
    {
        private readonly UserDBContext _context;

        public ProfileReadRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _context.Profiles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(Guid Id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Profile> GetProfileByUserIdAsync(Guid userId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
