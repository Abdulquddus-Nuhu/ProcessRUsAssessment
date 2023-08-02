using System;
namespace ProcessRUsAssessment.Shared.Responses
{
    public record LoginResponse
    {
        public string Message { get; set; } = "Login Successfull";
        public bool Status { get; set; } = true;
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool Result { get; set; } = false;
    }
}

