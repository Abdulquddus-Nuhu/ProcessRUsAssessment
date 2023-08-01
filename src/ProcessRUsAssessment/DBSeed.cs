using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProcessRUsAssessment.Data;
using ProcessRUsAssessment.Identity;

namespace ProcessRUsAssessment
{
    public class DBSeed : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DBSeed(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DBSeed>>();
            try
            {
                logger.LogInformation("Applying Migration!");
                await context.Database.MigrateAsync(cancellationToken: cancellationToken);
                logger.LogInformation("Migration Successful!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to apply Migration!");
            }
            var userManager = scope.ServiceProvider.GetService<UserManager<Persona>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
            try
            {
                logger.LogInformation("Seeding Data!");
                await IdentitySeed.SeedAsync(userManager, roleManager);
                await FruitsSeed.SeedFruitsAsync(context);
                logger.LogInformation("Seeding Successful!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to execute Data Seeding!");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}

