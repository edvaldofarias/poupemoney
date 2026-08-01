using PoupeMoney.Core.Domain.Entities.Account;
using PoupeMoney.Core.Domain.ValueObjects;

namespace PoupeMoney.Core.Commons.Domain.Entities.Account;

public static class AccountEntityFaker
{
    public static Faker<AccountEntity> Default() =>
        new Faker<AccountEntity>()
            .UsePrivateConstructor()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.OpeningBalance, f => new Amount(f.Random.Decimal(0, 1000)))
            .RuleFor(x => x.Overdraft, f => new Amount(f.Random.Decimal(0, 1000)))
            .RuleFor(x => x.Color, f => new Color("#FFF000"));
}
