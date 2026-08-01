using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PoupeMoney.Core.Application.Commons.Authentication;
using PoupeMoney.Core.Infrastructure.SqlServer.Context;
using PoupeMoney.Core.WebApi.Common.Authentication;

namespace PoupeMoney.Core.WebApi.Infrastructure.Firebase;

[ExcludeFromCodeCoverage]
internal static class FirebaseService
{
    internal static void AddFirebase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if(connectionString is null)
            throw new NullReferenceException("DefaultConnection is null");

        var apiKey = configuration.GetFirebaseKey();
        services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        services.DatabaseSetup(connectionString);
        services.FirebaseSetup(apiKey);
    }

    private static void FirebaseSetup(this IServiceCollection services, string apiKey)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://securetoken.google.com/{apiKey}";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"https://securetoken.google.com/{apiKey}",
                ValidateAudience = true,
                ValidAudience = apiKey,
                ValidateLifetime = true
            };
        });
    }

    private static void DatabaseSetup(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<PoupeMoneyContext>(options =>
                options.UseSqlServer(connectionString, config =>
                {
                    config.EnableRetryOnFailure();
                    config.UseRelationalNulls();
                }));
    }

    private static string GetConfiguration(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null)
            throw new NullReferenceException("DefaultConnection is null");

        return connectionString;
    }

    private static string GetFirebaseKey(this IConfiguration configuration)
    {
        var clientId = configuration.GetSection("Firebase:ApiKey").Value;
        return clientId is null ? throw new NullReferenceException("GoogleClientId is null") : clientId;
    }
}