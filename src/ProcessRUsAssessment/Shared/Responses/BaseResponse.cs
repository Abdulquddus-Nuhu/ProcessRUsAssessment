using System;
namespace ProcessRUsAssessment.Shared.Responses
{
    public record BaseResponse
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

