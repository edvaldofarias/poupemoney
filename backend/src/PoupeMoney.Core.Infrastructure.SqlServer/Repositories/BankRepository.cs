using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

public sealed class BankRepository(PoupeMoneyContext context) : IBankRepository
{
    public async Task<IEnumerable<BankEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var banks = await context.Bank.ToArrayAsync(cancellationToken);
        return banks;
    }

    public async Task<BankEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var bank = await context.Bank.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return bank;
    }
}