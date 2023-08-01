using System;
using Microsoft.AspNetCore.Identity;
using ProcessRUsAssessment.Identity;
using static ProcessRUsAssessment.Constants.StringConstants;

namespace ProcessRUsAssessment
{
    public class IdentitySeed
    {
        public static async Task SeedAsync(UserManager<Persona>? userManager, RoleManager<Role>? roleManager)
        {
            if (userManager is null || roleManager is null) return;

            if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.ADMIN});
            }
            if (!await roleManager.RoleExistsAsync(Roles.FRONTOFFICE))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.FRONTOFFICE});
            }
            if (!await roleManager.RoleExistsAsync(Roles.BACKOFFICE))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.BACKOFFICE});
            }
        }
    }
}

