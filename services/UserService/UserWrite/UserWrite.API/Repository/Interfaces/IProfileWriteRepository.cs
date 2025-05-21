namespace UserWrite.API.Repository.Interfaces
{
    public interface IProfileWriteRepository
    {
        Task CreateProfileAsync(ProfileCreateDto profile);
        Task UpdateProfileAsync(Guid userId, ProfileUpdateDto update);
        Task DeleteProfileAsync(Guid userId);
    }
}
