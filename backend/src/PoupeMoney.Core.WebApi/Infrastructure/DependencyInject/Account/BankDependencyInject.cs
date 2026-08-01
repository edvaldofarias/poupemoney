using PoupeMoney.Core.Application.Services;
using PoupeMoney.Core.Domain.Repositories;
using PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

namespace PoupeMoney.Core.WebApi.Infrastructure.DependencyInject.Account;

[ExcludeFromCodeCoverage]
public static class BankDependencyInject
{
    public static IServiceCollection AddBankDependencyInject(this IServiceCollection services)
    {
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IBankService, BankService>();

        return services;
    }
}