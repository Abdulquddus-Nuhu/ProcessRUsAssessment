using System;
using Microsoft.AspNetCore.Identity;

namespace ProcessRUsAssessment.Identity
{
    public class Persona : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => FirstName + " " + LastName;
    }
}

