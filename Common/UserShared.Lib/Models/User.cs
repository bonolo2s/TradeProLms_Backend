using System;
using System.ComponentModel.DataAnnotations;

namespace UserShared.Lib.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation , not mandatory but help me querrying  or updating mutlipe tables at same time.
        public Profile Profile { get; set; }
    }
}
