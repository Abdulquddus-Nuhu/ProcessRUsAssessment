﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProcessRUsAssessment;
using ProcessRUsAssessment.Data;
using static ProcessRUsAssessment.Constants.StringConstants;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Add Application Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"));
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 7139;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. **Enter Bearer Token Only**",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    x.EnableAnnotations();
    x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
});

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
});

//Authorization
//builder.Services.AddAuthorization(x =>
//{
//    x.AddPolicy(Roles.ADMIN, policy => policy.RequireRole(Roles.ADMIN));
//    x.AddPolicy(Roles.BACKOFFICE, policy => policy.RequireRole(Roles.BACKOFFICE));
//    x.AddPolicy(Roles.FRONTOFFICE, policy => policy.RequireRole(Roles.FRONTOFFICE));
//});

//Seed databse
builder.Services.AddHostedService<DBSeed>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
