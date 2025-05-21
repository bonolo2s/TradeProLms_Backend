﻿using System.ComponentModel.DataAnnotations;

namespace UserShared.Lib.ReqModels
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
