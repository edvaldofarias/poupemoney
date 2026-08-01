using PoupeMoney.Core.Application.Commons;
using PoupeMoney.Core.Application.Services;
using PoupeMoney.Core.WebApi.Infrastructure.DependencyInject.Account;
using PoupeMoney.Core.WebApi.Infrastructure.DependencyInject.Subscription;
using PoupeMoney.Core.WebApi.Infrastructure.Middlewares;

namespace PoupeMoney.Core.WebApi.Infrastructure.DependencyInject;

[ExcludeFromCodeCoverage]
public static class GlobalDependencyInject
{
    public static void AddDependencyInject(this IServiceCollection services)
    {
        services
            .AddSubscriptionDependencyInject()
            .AddAccountDependencyInject()
            .AddBankDependencyInject();

        services.AddScoped<ICommonService, CommonService>();
        services.AddTransient<ExceptionMiddleware>();
        services.AddScoped(typeof(Response<>), typeof(Response<>));
    }
}