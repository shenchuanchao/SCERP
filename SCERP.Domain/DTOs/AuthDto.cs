using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCERP.Domain.Models;
using SCERP.Identity;

namespace SCERP.Domain.DTOs
{
    /// <summary>
    /// 注册请求DTO
    /// </summary>
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }

    /// <summary>
    /// 登录请求DTO
    /// </summary>
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = true;

    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public ApplicationUser user { get; set; } = new();
    }


}
