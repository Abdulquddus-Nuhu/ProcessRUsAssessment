using System;
using ProcessRUsAssessment.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProcessRUsAssessment.Shared.Requests
{
    public record LoginRequest
    {
        [Required(ErrorMessage = "Please provide a value for User Name field")]
        [StringLength(255)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a value for password field")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
    }
}

