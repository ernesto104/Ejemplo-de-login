using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.API.Domain.Services.Communication
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
