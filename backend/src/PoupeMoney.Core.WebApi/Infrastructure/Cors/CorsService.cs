namespace PoupeMoney.Core.WebApi.Infrastructure.Cors;

[ExcludeFromCodeCoverage]
internal static class CorsService
{
    internal static void ConfigCors(this IServiceCollection services, IWebHostEnvironment environment)
    {
        var urls = environment.IsProduction()
            ? ["https://www.poupemoney.com.br", "http://www.poupemoney.com.br"]
            : new[] {"https://localhost:4200", "http://localhost:4200"};

        services.AddCors(options =>
            options.AddPolicy("PoupeMoney",
                builder =>
                {
                    if (environment.IsProduction())
                        builder.WithOrigins(urls);
                    else
                        builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithHeaders("Content-Type", "Accept", "authorization");
                    builder.AllowCredentials();
                }));
    }
}