using Microsoft.EntityFrameworkCore;
using UserShared.Lib.Models;

namespace UserWrite.API.Data.Interface
{
    public interface IUserDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Profile> Profiles { get; set }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
