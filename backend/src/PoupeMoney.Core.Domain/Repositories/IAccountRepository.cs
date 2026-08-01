using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Domain.Repositories;

public interface IAccountRepository
{
    Task CreateAsync(AccountEntity account, CancellationToken cancellationToken);

    Task UpdateAsync(AccountEntity account, CancellationToken cancellationToken);

    Task<AccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<AccountEntity>> GetAllBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken);
}