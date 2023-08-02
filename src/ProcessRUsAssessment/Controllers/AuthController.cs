using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using ProcessRUsAssessment.Services;
using ProcessRUsAssessment.Shared.Requests;
using ProcessRUsAssessment.Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;


namespace ProcessRUsAssessment.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [SwaggerOperation(
        Summary = "Register a new user endpoint",
        Description = "This endpoint creates a new user account. For role, FrontOffice = 0, BackOffice = 1, Admin = 2.",
        OperationId = "persona.create",
        Tags = new[] { "AuthEndpoints" })
         ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse>> CreatePersonaAsync([FromBody] PersonaRequest request)
        {
            var response = await _authService.CreatePersonaAsync(request);
            if (!response.Status)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [SwaggerOperation(
        Summary = "Authenticates a user and generate JWT Token endpoint",
        Description = "Authenticates a user and generate JWT Token",
        OperationId = "auth.login",
        Tags = new[] { "AuthEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
        {
            var loginResponse = await _authService.LoginAsync(request);
            if (!loginResponse.Status)
            {
                return BadRequest(loginResponse);
            }
            return Ok(loginResponse);
        }
    }
}

