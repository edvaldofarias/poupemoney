namespace PoupeMoney.Core.Application.Services.Interfaces;

public interface ICommonService
{
    Task<Guid> GetSubscriptionIdAsync(CancellationToken cancellationToken);
}
