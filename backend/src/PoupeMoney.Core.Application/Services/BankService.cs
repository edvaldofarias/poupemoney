using PoupeMoney.Core.Application.Queries.Bank;

namespace PoupeMoney.Core.Application.Services;

public sealed class BankService(IBankRepository bankRepository) : IBankService
{
    public async Task<IEnumerable<BankQuery>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var banks = await bankRepository.GetAllAsync(cancellationToken);
        return banks.Select(bank => new BankQuery(bank.Id, bank.Name, bank.Code));
    }

    public async Task<BankQuery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var bank = await bankRepository.GetByIdAsync(id, cancellationToken);
        return bank is null ? 
        null : 
        new BankQuery(bank.Id, bank.Name, bank.Code);
    }
}
