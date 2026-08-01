namespace PoupeMoney.Core.Domain.Entities.Account;

public sealed class BankEntity : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private BankEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public BankEntity(string name, int code)
    {
        Name = name;
        Code = code;
        Validate();
    }

    private readonly List<AccountEntity> _accounts = [];
    public string Name { get; private set; }
    public int Code { get; private set; }
    public IReadOnlyList<AccountEntity> Accounts => _accounts;

    public BankEntity AddAccount(AccountEntity account)
    {
        _accounts.Add(account);
        return this;
    }

    protected override void Validate()
    {
        DomainException.When(string.IsNullOrEmpty(Name), $"{nameof(Name)} is Invalid");
    }
}