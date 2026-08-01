using PoupeMoney.Core.Domain.Entities.Subscription;
using PoupeMoney.Core.Domain.ValueObjects;

namespace PoupeMoney.Core.Domain.Entities.Account;

public sealed class AccountEntity : BaseEntity
{
    public AccountEntity(
        string name,
        string? description,
        Amount openingBalance,
        Amount overdraft,
        Color color,
        Guid subscriptionId,
        Guid bankId)
    {
        Name = name;
        Description = description;
        OpeningBalance = openingBalance;
        Overdraft = overdraft;
        Color = color;
        SubscriptionId = subscriptionId;
        BankId = bankId;
        Validate();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AccountEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Amount OpeningBalance { get; private set; }
    public Amount Overdraft { get; private set; }
    public Color Color { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public SubscriptionEntity Subscription { get; private set; } = default!;
    public Guid BankId { get; private set; }
    public BankEntity Bank { get; private set; } = default!;

    public void Update(
        string name,
        string? description,
        decimal openingBalance,
        decimal overdraft,
        Color color)
    {
        Name = name;
        Description = description;
        OpeningBalance = openingBalance;
        Overdraft = overdraft;
        Color = color;
        this.Update();
        Validate();
    }

    protected override void Validate()
    {
        DomainException.When(string.IsNullOrEmpty(Name), $"Invalid {nameof(Name)} is required");
        DomainException.When(Name.Length > 100, $"Invalid {nameof(Name)}. too long, maximum 100 charecters");
        DomainException.When(Description is not null && Description.Length > 1024, $"Invalid {nameof(Description)}. too long, maximum 1024 charecters");
    }
}