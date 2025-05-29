using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;

namespace UserWrite.API.Repository.Interfaces
{
    public interface IProfileWriteRepository
    {
        Task<Profile> CreateProfileAsync(ProfileCreateDto profile);
        Task<Profile> UpdateProfileAsync(ProfileUpdateDto update);
        Task DeleteProfileAsync(Guid userId); // ill do it internally via delete user

    }
}
