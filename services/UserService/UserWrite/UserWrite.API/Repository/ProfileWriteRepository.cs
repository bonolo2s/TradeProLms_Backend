using Microsoft.EntityFrameworkCore;
using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;
using UserWrite.API.Data;
using UserWrite.API.Repository.Interfaces;

namespace UserWrite.API.Repository
{
    public class ProfileWriteRepository : IProfileWriteRepository
    {
        private readonly UserDBContext _context;

        public ProfileWriteRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<Profile> CreateProfileAsync(ProfileCreateDto dto)
        {
            var profile = new Profile
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Bio = dto.Bio,
                ProfilePictureUrl = dto.ProfilePictureUrl
            };

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return profile;
        }


        public async Task<Profile> UpdateProfileAsync(ProfileUpdateDto dto)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == dto.UserId);
            if (profile == null)
            {
                throw new Exception("Profile not found.");
            }

            profile.FirstName = dto.FirstName ?? profile.FirstName;
            profile.LastName = dto.LastName ?? profile.LastName;
            profile.PhoneNumber = dto.PhoneNumber ?? profile.PhoneNumber;
            profile.Bio = dto.Bio ?? profile.Bio;
            profile.ProfilePictureUrl = dto.ProfilePictureUrl ?? profile.ProfilePictureUrl;

            await _context.SaveChangesAsync();

            return profile;
        }

        public async Task DeleteProfileAsync(Guid userId)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
            {
                throw new Exception("Profile not found.");
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}
