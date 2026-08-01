using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

public sealed class AccountRepository(PoupeMoneyContext context) : IAccountRepository
{
    public async Task CreateAsync(AccountEntity account, CancellationToken cancellationToken)
    {
        await context.Account.AddAsync(account, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AccountEntity account, CancellationToken cancellationToken)
    {
        context.Account.Update(account);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IEnumerable<AccountEntity>> GetAllBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken)
    {
        var accounts = await context.Account.Where(account => account.SubscriptionId == subscriptionId).ToListAsync(cancellationToken);
        return accounts;
    }

    public Task<AccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = context.Account.FirstOrDefaultAsync(account => account.Id == id, cancellationToken);
        return account;
    }
}