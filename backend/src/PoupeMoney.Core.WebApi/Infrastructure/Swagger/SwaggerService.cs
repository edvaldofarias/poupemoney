using System.Reflection;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoupeMoney.Core.WebApi.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
internal static class SwaggerService
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerDefaultValues>();
            c.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
            c.AddSecurityRequirement(GetOpenApiSecurityRequirement());
            c.AddSwaggerXmlComments();
        });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }

    private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT autorização usando header com Bearer"
        };
    }

    private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                },
                Array.Empty<string>()
            }
        };
    }

    private static void AddSwaggerXmlComments(this SwaggerGenOptions swaggerGenOptions)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            swaggerGenOptions.IncludeXmlComments(xmlPath);
    }

    internal static void ConfigSwagger(
        this WebApplication app,
        IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = $"PoupeMoney.Core {description.GroupName.ToUpperInvariant()}";
                c.SwaggerEndpoint(url, name);
            }
        });
    }
}