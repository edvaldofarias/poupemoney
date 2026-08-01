namespace PoupeMoney.Core.WebApi.Infrastructure.Version;

[ExcludeFromCodeCoverage]
internal static class VersionService
{
    internal static void AddVersion(this IServiceCollection services)
    {
        var builderVersion = services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        builderVersion.AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}