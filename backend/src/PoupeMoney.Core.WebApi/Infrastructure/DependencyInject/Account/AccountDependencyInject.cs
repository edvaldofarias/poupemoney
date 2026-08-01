using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Application.Commands.Account.Validations;
using PoupeMoney.Core.Application.Services;
using PoupeMoney.Core.Domain.Repositories;
using PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

namespace PoupeMoney.Core.WebApi.Infrastructure.DependencyInject.Account;

[ExcludeFromCodeCoverage]
public static class AccountDependencyInject
{
    public static IServiceCollection AddAccountDependencyInject(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IValidator<AccountCreateCommand>, AccountCreateValidation>();
        services.AddScoped<IValidator<AccountUpdateCommand>, AccountUpdateValidation>();
        return services;
    }
}