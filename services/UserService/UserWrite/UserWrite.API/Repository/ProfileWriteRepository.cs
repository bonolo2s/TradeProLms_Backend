using UserShared.Lib.ReqModels;
using UserWrite.API.Repository.Interfaces;

namespace UserWrite.API.Repository
{
    public class ProfileWriteRepository : IProfileWriteRepository
    {
        public Task CreateProfileAsync(ProfileCreateDto profile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProfileAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfileAsync(ProfileUpdateDto update)
        {
            throw new NotImplementedException();
        }
    }
}
