using PoupeMoney.Core.Application.Commands.Subscription;
using PoupeMoney.Core.Application.Commons;


namespace PoupeMoney.Core.Application.Services.Interfaces;
public interface ISubscriptionService
{
    Task<Response> CreateAsync(SubscriptionCreateCommand command, CancellationToken cancellationToken);
}
