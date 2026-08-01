using PoupeMoney.Core.Application.Queries.Bank;

namespace PoupeMoney.Core.Application.Services.Interfaces;

public interface IBankService
{
    Task<IEnumerable<BankQuery>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BankQuery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
