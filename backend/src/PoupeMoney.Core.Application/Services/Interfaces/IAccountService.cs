using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Application.Commons;
using PoupeMoney.Core.Application.Queries.Account;

namespace PoupeMoney.Core.Application.Services.Interfaces;

public interface IAccountService
{
    Task<Response<Guid>> CreateAsync(AccountCreateCommand command, CancellationToken cancellationToken = default);

    Task<Response> UpdateAsync(AccountUpdateCommand command, CancellationToken cancellationToken = default);

    Task<AccountQuery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<AccountQuery>> GetAllAsync(CancellationToken cancellationToken = default);
}
