using PoupeMoney.Core.Domain.Entities.Subscription;

namespace PoupeMoney.Core.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task CreateAsync(SubscriptionEntity subscriptionEntity, CancellationToken cancellationToken);
    Task<SubscriptionEntity?> GetByIdAsync(string googleId, CancellationToken cancellationToken = default);
}