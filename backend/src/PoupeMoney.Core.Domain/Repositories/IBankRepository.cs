using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Domain.Repositories;

public interface IBankRepository
{
    Task<BankEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<BankEntity>> GetAllAsync(CancellationToken cancellationToken);
}
