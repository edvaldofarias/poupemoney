using System.Text.Json;

using FluentValidation.Results;

namespace PoupeMoney.Core.WebApi.Infrastructure.Middlewares;

[ExcludeFromCodeCoverage]
internal sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            const string message = "Exception caught in global error handler.";
            logger.LogError(e, message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), httpContext.RequestAborted);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            DirectoryNotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ValidationException => "Validation Failure",
            _ => "Server Error"
        };

    private static IEnumerable<ValidationFailure> GetErrors(Exception exception)
    {
        IEnumerable<ValidationFailure> errors = default!;
        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }
}