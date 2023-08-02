using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ProcessRUsAssessment.Models;
using Microsoft.AspNetCore.Identity;
using ProcessRUsAssessment.Identity;

namespace ProcessRUsAssessment.Data
{
    public class AppDbContext : IdentityDbContext<Persona, Role, int>
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<Fruit> Fruits { get; set; }
    }
}

