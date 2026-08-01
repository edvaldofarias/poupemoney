namespace PoupeMoney.Core.WebApi.Infrastructure.Middlewares;

[ExcludeFromCodeCoverage]
public static class GeneralMiddleware
{
    public static void AddExceptionGlobalHandler(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionMiddleware>();
}