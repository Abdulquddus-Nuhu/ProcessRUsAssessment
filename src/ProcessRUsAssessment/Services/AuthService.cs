using System;
using Microsoft.AspNetCore.Identity;
using ProcessRUsAssessment.Identity;
using ProcessRUsAssessment.Shared.Enums;
using ProcessRUsAssessment.Shared.Requests;
using ProcessRUsAssessment.Shared.Responses;
using static ProcessRUsAssessment.Constants.StringConstants;

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

        public async Task<BaseResponse> CreatePersonaAsync(PersonaRequest request)
        {
            _logger.LogInformation("Creating a new user");
            var response = new BaseResponse();
            var user = new Persona() { UserName = request.Email, Email = request.Email,  FirstName = request.FirstName, LastName = request.LastName, EmailConfirmed = true };
            var creationResult = await _userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                response.Message = string.Join(',', creationResult.Errors.Select(a => a.Description));
                _logger.LogInformation("User Creation is not successful with the following error {0}", response.Message);
                return response;
            }

            _logger.LogInformation("User Creation is successful");

            string roleToAdd = string.Empty;
            switch (request.Role)
            {
                case RoleType.FrontOffice:
                    roleToAdd = Roles.FRONTOFFICE;
                    break;
                case RoleType.BackOffice:
                    roleToAdd = Roles.BACKOFFICE;
                    break;
                case RoleType.Admin:
                    roleToAdd = Roles.ADMIN;
                    break;
                default:
                    break;
            }
            var roleResult = await _userManager.AddToRoleAsync(user, roleToAdd);
            response.Message = "Account Created Successfully";

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse();

            _logger.LogInformation("A user with email {0} is trying to login", request.Email);

            var loginResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            response.Result = loginResult.Succeeded;
            response.Email = request.Email;

            if (!loginResult.Succeeded)
            {
                response.Status = false;
                response.Message = "Authentication failed!";
                return response;
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogWarning("Unable to find user {0} after a successfull Sign in", request.Email);
                response.Status = false;
                response.Message = "Authentication failed!";
                return response;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            //(response.Token) = await _tokenService.GetTokenAsync(new PersonaResponse() { UserName = user.UserName, Email = user.Email, FirstName = user.FirstName, Id = user.Id, LastName = user.LastName, PhoneNumber = user.PhoneNumber, Roles = userRoles, Tin = user.Tin, IsActive = user.IsActive }, request.RememberMe);

            return response;

        }
    }
}

