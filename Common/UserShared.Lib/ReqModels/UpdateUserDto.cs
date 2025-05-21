using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserShared.Lib.ReqModels
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
