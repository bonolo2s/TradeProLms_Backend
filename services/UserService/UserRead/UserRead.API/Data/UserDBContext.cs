using Microsoft.EntityFrameworkCore;
using UserRead.API.Data.Interface;
using UserShared.Lib.Models;

namespace UserRead.API.Data
{
    public class UserDBContext : DbContext, IUserDBContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
