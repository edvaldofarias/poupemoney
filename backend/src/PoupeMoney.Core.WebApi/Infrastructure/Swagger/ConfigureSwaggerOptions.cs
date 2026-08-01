using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoupeMoney.Core.WebApi.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescription) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in apiVersionDescription.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateOpenApiInfo(description));
        }
    }

    private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "PoupeMoney - Core",
            Version = description.ApiVersion.ToString(),
            Description = "API de Core do PoupeMoney",
            Contact = new OpenApiContact { Name = "PoupeMoney", Email = "edvaldofariasdesantana@hotmail.com"}
        };

        if (description.IsDeprecated)
            info.Description = $"{info.Description} - Esta versão está obsoleta!";

        return info;
    }
}