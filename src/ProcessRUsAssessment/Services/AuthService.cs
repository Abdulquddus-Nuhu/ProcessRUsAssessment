using System;
using Microsoft.AspNetCore.Identity;
using ProcessRUsAssessment.Identity;
using ProcessRUsAssessment.Shared.Requests;
using ProcessRUsAssessment.Shared.Responses;

namespace ProcessRUsAssessment.Services
{
    public class AuthService
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<Persona> userManager, SignInManager<Persona> signInManager,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse();

            _logger.LogInformation("A user with name {0} is trying to login", request.UserName);

            var loginResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);

            response.Result = loginResult.Succeeded;
            response.UserName = request.UserName;

            if (!loginResult.Succeeded)
            {
                response.Status = false;
                response.Message = "Authentication failed!";
                return response;
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                _logger.LogWarning("Unable to find user {0} after a successfull log in", request.UserName);
                response.Status = false;
                response.Message = "Authentication failed!";
                return response;
            }

            //(response.Token) = await _tokenService.GetTokenAsync(new PersonaResponse() { UserName = user.UserName, Email = user.Email, FirstName = user.FirstName, Id = user.Id, LastName = user.LastName, PhoneNumber = user.PhoneNumber, Roles = userRoles, Tin = user.Tin, IsActive = user.IsActive }, request.RememberMe);

            return response;

        }
    }
}

