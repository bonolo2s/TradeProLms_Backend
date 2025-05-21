using System;
using UserShared.Lib.Models;
using UserWrite.API.Data;

public static class ProfileSeeder
{
    public static void Seed(UserDBContext context, List<User> seededUsers)
    {
        if (!context.Profiles.Any())
        {
            var aliceUser = seededUsers.FirstOrDefault(u => u.Username == "Alice");
            var bobUser = seededUsers.FirstOrDefault(u => u.Username == "Bob");

            if (aliceUser != null && bobUser != null)
            {
                var profiles = new List<Profile>
            {
                new Profile
                {
                    Id = Guid.NewGuid(),
                    UserId = aliceUser.Id,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    PhoneNumber = "+27821234567",
                    DateOfBirth = new DateTime(1995, 4, 12),
                    Bio = "Full-stack developer & passionate trader.",
                    ProfilePictureUrl = "https://example.com/images/alice.jpg"
                },
                new Profile
                {
                    Id = Guid.NewGuid(),
                    UserId = bobUser.Id,
                    FirstName = "Bob",
                    LastName = "Smith",
                    PhoneNumber = "+27829876543",
                    DateOfBirth = new DateTime(1993, 9, 25),
                    Bio = "Tech enthusiast with a love for DevOps and cloud architecture.",
                    ProfilePictureUrl = "https://example.com/images/bob.jpg"
                }
            };

                context.Profiles.AddRange(profiles);
                context.SaveChanges();
            }
        }
    }

}
