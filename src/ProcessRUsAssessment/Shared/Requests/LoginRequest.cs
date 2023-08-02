using System;
using ProcessRUsAssessment.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProcessRUsAssessment.Shared.Requests
{
    public record LoginRequest
    {
        [Required(ErrorMessage = "Please provide a value for Email Address field")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a value for password field")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }
}

