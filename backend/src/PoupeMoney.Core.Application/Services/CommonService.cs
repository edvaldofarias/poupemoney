namespace PoupeMoney.Core.Application.Services;

public sealed class CommonService(
    IAuthenticatedUser authenticatedUser,
    ISubscriptionRepository subscriptionRepository,
    ILogger<CommonService> logger) : ICommonService
{
    public async Task<Guid> GetSubscriptionIdAsync(CancellationToken cancellationToken)
    {
        var googleId = authenticatedUser.Id;
        var subscriptionEntity = await subscriptionRepository.GetByIdAsync(googleId, cancellationToken);

        if (subscriptionEntity is not null)
            return subscriptionEntity.Id;

        logger.LogWarning("Subscription not found for user {GoogleId}", googleId);
        throw new UnauthorizedAccessException("Subscription not found");
    }
}