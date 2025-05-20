using UserShared.Lib.Models;

namespace UserWrite.API.Data
{
    public static class UserSeeder
    {
        public static void Seed(UserDBContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<User>
                {
                    new User { Id = Guid.NewGuid(), Username = "Alice", Email = "alice@example.com", PasswordHash = "hashed-password" },
                    new User { Id = Guid.NewGuid(), Username = "Bob", Email = "bob@example.com", PasswordHash = "hashed-password" },
                });

                context.SaveChanges();
            }
        }
    }
}
