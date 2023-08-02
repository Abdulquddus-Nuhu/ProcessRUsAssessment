using System;
using Microsoft.AspNetCore.Identity;
using ProcessRUsAssessment.Identity;
using static ProcessRUsAssessment.Constants.StringConstants;

namespace ProcessRUsAssessment
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(UserManager<Persona>? userManager, RoleManager<Role>? roleManager)
        {
            if (userManager is null || roleManager is null) return;

            await SeedRoles(roleManager);

            if (await userManager.FindByNameAsync("Admin") is null)
            {
                var admin = new Persona()
                {
                    UserName = "Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "AdminP@ssword");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.ADMIN);
                }
            }

            if (await userManager.FindByNameAsync("FrontOffice") is null)
            {
                var frontOffice = new Persona()
                {
                    UserName = "FrontOffice",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(frontOffice, "FrontOfficeP@ssword");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(frontOffice, Roles.FRONTOFFICE);
                }
            }

            if (await userManager.FindByNameAsync("BackOffice") is null)
            {
                var backOffice = new Persona()
                {
                    UserName = "BackOffice",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(backOffice, "BackOfficeP@ssword");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(backOffice, Roles.BACKOFFICE);
                }
            }

        }

        private static async Task SeedRoles(RoleManager<Role>? roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.ADMIN });
            }
            if (!await roleManager.RoleExistsAsync(Roles.FRONTOFFICE))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.FRONTOFFICE });
            }
            if (!await roleManager.RoleExistsAsync(Roles.BACKOFFICE))
            {
                await roleManager.CreateAsync(new Role() { Name = Roles.BACKOFFICE });
            }
        }
    }
}

