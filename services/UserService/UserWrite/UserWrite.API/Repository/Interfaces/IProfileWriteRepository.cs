using UserShared.Lib.ReqModels;

namespace UserWrite.API.Repository.Interfaces
{
    public interface IProfileWriteRepository
    {
        Task CreateProfileAsync(ProfileCreateDto profile);
        Task UpdateProfileAsync(ProfileUpdateDto update);
        Task DeleteProfileAsync(Guid userId); // ill do it internally

    }
}
