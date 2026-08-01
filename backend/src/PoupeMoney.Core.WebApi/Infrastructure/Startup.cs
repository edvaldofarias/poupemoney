using PoupeMoney.Core.WebApi.Infrastructure.Cors;
using PoupeMoney.Core.WebApi.Infrastructure.Culture;
using PoupeMoney.Core.WebApi.Infrastructure.DependencyInject;
using PoupeMoney.Core.WebApi.Infrastructure.Firebase;
using PoupeMoney.Core.WebApi.Infrastructure.Middlewares;
using PoupeMoney.Core.WebApi.Infrastructure.Swagger;
using PoupeMoney.Core.WebApi.Infrastructure.Version;

namespace PoupeMoney.Core.WebApi.Infrastructure;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static void AddServices(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddVersion();
        services.AddSwagger();
        services.AddCulture();

        services.ConfigCors(environment);
        services.AddFirebase(configuration);

        services.AddHttpClient();

        services.AddHttpContextAccessor();
        services.AddDependencyInject();

        if (configuration.GetValue<bool>("WarmUp"))
            services.AddHostedService<WarmUp>();
    }

    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseDeveloperExceptionPage();
            app.ConfigSwagger(provider);
        }

        app.AddExceptionGlobalHandler();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}