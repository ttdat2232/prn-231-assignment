﻿using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AuthenticateRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(1)]
        public string Password { get; set; } = string.Empty;
    }
}
