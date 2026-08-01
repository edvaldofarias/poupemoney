using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoupeMoney.Core.WebApi.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public sealed class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Parameters is null)
            return;

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => string.Equals(p.Name, parameter.Name, StringComparison.Ordinal));

            parameter.Description ??= description.ModelMetadata.Description;

            if (parameter.Schema.Default is null && description.DefaultValue is not null)
            {
                parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
            }

            parameter.Required |= description.IsRequired;
        }
    }
}