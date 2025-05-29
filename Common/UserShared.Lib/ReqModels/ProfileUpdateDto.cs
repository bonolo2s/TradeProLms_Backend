using System;
using System.ComponentModel.DataAnnotations;

namespace UserShared.Lib.ReqModels
{
    public class ProfileUpdateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(255)]
        public string? ProfilePictureUrl { get; set; }
    }
}
