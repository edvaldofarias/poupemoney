using PoupeMoney.Core.Domain.Entities.Account;
using PoupeMoney.Core.Domain.ValueObjects;

namespace PoupeMoney.Core.Domain.Entities.Subscription;

public sealed class SubscriptionEntity : BaseEntity
{
    private readonly List<AccountEntity> _accounts = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public SubscriptionEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    public SubscriptionEntity(
        string userId,
        Email email,
        DateOnly dateBirth,
        Gender gender,
        string? other)
    {
        UserId = userId;
        Email = email;
        DateBirth = dateBirth;
        Gender = gender;
        Other = other;

        Validate();
    }

    public string UserId { get; private set; }

    public Email Email { get; private set; }

    public Gender Gender { get; private set; }

    public string? Other { get; private set; }

    public DateOnly DateBirth { get; private set; }

    public IReadOnlyCollection<AccountEntity> Accounts => _accounts.AsReadOnly();

    public void AddAccount(AccountEntity account)
    {
        _accounts.Add(account);
    }

    protected override void Validate()
    {
        DomainException.When(Other is not null && Other.Length > 100, $"{nameof(Other)} is invalid. too large, maximum 100 charectes");
        DomainException.When(DateBirth > DateOnly.FromDateTime(DateTime.Now.AddYears(-18)), $"{nameof(DateBirth)} is invalid");
    }
}