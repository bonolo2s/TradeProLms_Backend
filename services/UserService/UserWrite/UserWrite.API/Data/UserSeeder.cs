using UserShared.Lib.Models;

namespace UserWrite.API.Data
{
    public static class UserSeeder
    {
        public static List<User> Seed(UserDBContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid(), Username = "Alice", Email = "alice@example.com", PasswordHash = "hashed-password" },
                    new User { Id = Guid.NewGuid(), Username = "Bob", Email = "bob@example.com", PasswordHash = "hashed-password" },
                };

                context.Users.AddRange(users);
                context.SaveChanges();

                return users;
            }

            return context.Users.ToList();
        }
    }
}
