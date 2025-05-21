using System.ComponentModel.DataAnnotations;


namespace UserShared.Lib.ReqModels
{
    public class PasswordResetDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public string ResetToken { get; set; }
    }

}
