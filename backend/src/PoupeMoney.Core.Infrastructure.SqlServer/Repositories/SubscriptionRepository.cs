using PoupeMoney.Core.Domain.Entities.Subscription;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Repositories;

public sealed class SubscriptionRepository(PoupeMoneyContext context) : ISubscriptionRepository
{
    public async Task CreateAsync(SubscriptionEntity subscriptionEntity, CancellationToken cancellationToken)
    {
        await context.Subscription.AddAsync(subscriptionEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<SubscriptionEntity?> GetByIdAsync(string googleId, CancellationToken cancellationToken = default)
    {
        var subscriptionEntity = await context.Subscription.FirstOrDefaultAsync(x => x.UserId == googleId, cancellationToken);
        return subscriptionEntity;
    }
}