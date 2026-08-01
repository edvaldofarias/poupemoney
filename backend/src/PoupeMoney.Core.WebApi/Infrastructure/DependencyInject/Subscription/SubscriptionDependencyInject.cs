using PoupeMoney.Core.Application.Commands.Subscription;
using PoupeMoney.Core.Application.Commands.Subscription.Validations;
using PoupeMoney.Core.Application.Services;
using PoupeMoney.Core.Domain.Repositories;
using PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

namespace PoupeMoney.Core.WebApi.Infrastructure.DependencyInject.Subscription;

[ExcludeFromCodeCoverage]
public static class SubscriptionDependencyInject
{
    public static IServiceCollection AddSubscriptionDependencyInject(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IValidator<SubscriptionCreateCommand>, SubscriptionCreateValidation>();
        return services;
    }
}