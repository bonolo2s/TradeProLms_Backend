using UserShared.Lib.Models;

namespace UserRead.API.Repository.Interfaces
{
    public interface IProfileReadRepository
    {
        Task<Profile> GetProfileByIdAsync(Guid Id);

        Task<Profile> GetProfileByUserIdAsync(Guid userId);

        Task<IEnumerable<Profile>> GetAllProfilesAsync(int pageNumber = 1, int pageSize = 20);
    }
}
