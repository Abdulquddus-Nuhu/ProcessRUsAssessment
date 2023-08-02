using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return response;
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

