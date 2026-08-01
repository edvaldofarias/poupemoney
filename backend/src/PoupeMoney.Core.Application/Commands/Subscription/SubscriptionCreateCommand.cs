namespace PoupeMoney.Core.Application.Commands.Subscription;
public sealed class SubscriptionCreateCommand
{
    public DateOnly DateBirth { get; init; }
    public Gender Gender { get; init; }
    public string? Other { get; init; }
}
