using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserShared.Lib.ReqModels
{
    public class ProfileUpdateDto
    {
        [Required]
        public Guid UserId { get; set; } // ill look up the profile

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        [MaxLength(255)]
        public string ProfilePictureUrl { get; set; }
    }

}
