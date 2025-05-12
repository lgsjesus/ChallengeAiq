using System.Text;
using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Challenge.Process.Aiq.Services;
using Challenge.Process.Aiq.WebApi.Abstractions;
using Challenge.Process.Aiq.WebApi.Abstractions.SwaggerExamples;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.Process.Aiq.WebApi.Configuration;

public static class WebApiConfiguration
{
    public static void RegisterConfigurations(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = new LowerCasePolicy();
        });
        
       services.AddControllers();
       services.AddEndpointsApiExplorer();
       services.AddSwaggerGen();
       RegisterDatabase(services, configuration);
  
       services.AddSwaggerGen(options =>
       {
            options.AddSwaggerExamples();
            options.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "WebApi Challenge",
                Description = "Api created to management favorite products from customer.",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Luiz Guilherme Silva de Jesus",
                    Email = "lguilherme.j@gmail.com"
                } });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor, forneça um token JWT válido no seguinte formato: <strong><em>Bearer [JWT Token]</em></strong>",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    Array.Empty<string>()
                }
            });
        });
       
       services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
               };
           });

        services.RegisterServices(configuration);
    }

    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            throw new UserException(nameof(connectionString));
        
        services.AddDbContext<ChallengeProcessAiqDbContext>(
            dbContextOptions => dbContextOptions
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        services.RegisterDbService();
    }
}