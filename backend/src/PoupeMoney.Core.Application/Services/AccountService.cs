using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Application.Commands.Account.Validations;
using PoupeMoney.Core.Application.Commons;
using PoupeMoney.Core.Application.Queries.Account;
using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Application.Services;

public sealed class AccountService(
    ILogger<AccountService> logger,
    IAccountRepository accountRepository,
    ICommonService commonService) : IAccountService
{
    public async Task<Response<Guid>> CreateAsync(AccountCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validate = await new AccountCreateValidation().ValidateAsync(command, cancellationToken);
        var response = new Response<Guid>();

        if (validate.IsValid is false)
        {
            logger.LogWarning("Validation failed for command {@Command}", command);
            response.AddError(validate.Errors.ToArray());
            return response;
        }

        var subscriptionId = await commonService.GetSubscriptionIdAsync(cancellationToken);

        var id = await CreateAccountAsync(command, subscriptionId, cancellationToken);
        return response.AddData(id);
    }

    public async Task<Response> UpdateAsync(AccountUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validate = await new AccountUpdateValidation().ValidateAsync(command, cancellationToken);
        var account = await accountRepository.GetByIdAsync(command.Id, cancellationToken);
        var response = new Response();

        if (validate.IsValid && account is null)
            validate.Errors.Add(
                new ValidationFailure(nameof(command.Id), "Account not found"));

        if (validate.IsValid is false)
        {
            logger.LogWarning("Validation failed for command {@Command}", command);
            response.AddError(validate.Errors.ToArray());
            return response;
        }

        account!.Update(command.Name, command.Description, command.OpeningBalance, command.Overdraft, command.Color);
        await accountRepository.UpdateAsync(account, cancellationToken);

        return response;
    }

    public async Task<AccountQuery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await accountRepository.GetByIdAsync(id, cancellationToken);
        return account is null
            ? null
            : new AccountQuery(
                account.Id, account.Name, account.Description,
                account.OpeningBalance, account.Color, account.SubscriptionId,
                account.BankId);
    }

    public async Task<IEnumerable<AccountQuery>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var subscriptionId = await commonService.GetSubscriptionIdAsync(cancellationToken);
        var accounts = await accountRepository.GetAllBySubscriptionIdAsync(subscriptionId, cancellationToken);
        return accounts.Select(account => new AccountQuery(
            account.Id, account.Name, account.Description,
            account.OpeningBalance, account.Color, account.SubscriptionId,
            account.BankId));
    }

    #region Private Methods

    private async Task<Guid> CreateAccountAsync(AccountCreateCommand command, Guid subscriptionId,
        CancellationToken cancellationToken)
    {
        var account = new AccountEntity(
            command.Name,
            command.Description,
            command.OpeningBalance,
            command.Overdraft,
            command.Color,
            subscriptionId,
            command.BankId);

        await accountRepository.CreateAsync(account, cancellationToken);
        return account.Id;
    }

    #endregion
}