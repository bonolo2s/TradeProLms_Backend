using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserShared.Lib.Models;

namespace UserRead.API.Data.Interface
{
    public interface IUserDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
