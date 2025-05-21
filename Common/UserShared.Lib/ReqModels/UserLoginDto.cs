using System.ComponentModel.DataAnnotations;

namespace UserShared.Lib.ReqModels
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
