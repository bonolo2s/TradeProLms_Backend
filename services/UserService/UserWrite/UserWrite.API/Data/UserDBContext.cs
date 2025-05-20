using Microsoft.EntityFrameworkCore;
using UserShared.Lib.Models;
using UserWrite.API.Data.Interface;

namespace UserWrite.API.Data
{
    public class UserDBContext : DbContext, IUserDBContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) // options from my entry to base(EF/DBCOntext)
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
