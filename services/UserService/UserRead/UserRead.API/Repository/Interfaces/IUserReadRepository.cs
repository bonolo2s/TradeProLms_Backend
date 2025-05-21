using UserShared.Lib.Models;

namespace UserRead.API.Repository.Interfaces
{
    public interface IUserReadRepository
    {
        Task<User> GetUserByIdAsync(Guid Id);

        Task<User> GetUserByUsernameAsync(string username);

        Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 20);

        Task<bool> UserExistsAsync(string username, string email);
    }
}
