using System;
using ProcessRUsAssessment.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProcessRUsAssessment.Shared.Requests
{
    public class PersonaRequest
    {
        [Required(ErrorMessage = "Please provide a value for Email Address field")]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a value for First Name field")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a value for Last Name field")]
        public string LastName { get; set; } = string.Empty;
        public RoleType Role { get; set; }

        [Required(ErrorMessage = "Please provide a value for password field"), MinLength(8, ErrorMessage = "Password must consist of at least 8 characters")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a value for the confirm password field"), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [StringLength(255)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}

