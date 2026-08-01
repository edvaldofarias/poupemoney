using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Application.Commands.Subscription;
using PoupeMoney.Core.Application.Commands.Subscription.Validations;
using PoupeMoney.Core.Application.Commons;
using PoupeMoney.Core.Domain.Entities.Subscription;

namespace PoupeMoney.Core.Application.Services;
public sealed class SubscriptionService(
    IAuthenticatedUser authenticatedUser,
    IAccountService accountService,
    ISubscriptionRepository subscriptionRepository,
    ILogger<SubscriptionService> logger) : ISubscriptionService
{
    public async Task<Response> CreateAsync(SubscriptionCreateCommand command, CancellationToken cancellationToken)
    {
        var response = new Response();
        var validator = new SubscriptionCreateValidation();
        var validate = await validator.ValidateAsync(command, cancellationToken);

        if (validate.IsValid is false)
        {
            logger.LogInformation("Error on create subscription - {@SubscriptionCreateRequest} - with errors {@Errors}", command, validate.Errors);
            response.AddError(validate.Errors);
            
            return response;
        }

        var subscription = await CreateSubscriptionAsync(command, cancellationToken);
        await CreateDefaultAccountAsync(cancellationToken);

        logger.LogInformation("Created subscription - {@SubscriptionId} - with success", subscription.Id);
        return response;
    }

    private async Task<SubscriptionEntity> CreateSubscriptionAsync(SubscriptionCreateCommand command, CancellationToken cancellationToken)
    {
        var googleId = authenticatedUser.Id;
        var email = authenticatedUser.Email;
        var subscription = new SubscriptionEntity(googleId, email, command.DateBirth, command.Gender, command.Other);
        await subscriptionRepository.CreateAsync(subscription, cancellationToken);
        return subscription;
    }

    private async Task CreateDefaultAccountAsync(CancellationToken cancellationToken)
    {
        var accountCreateCommand = new AccountCreateCommand();
        await accountService.CreateAsync(accountCreateCommand, cancellationToken);
        logger.LogInformation("Created default account for subscription");
    }
}
