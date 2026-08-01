using PoupeMoney.Core.Application.Commands.Account;

namespace PoupeMoney.Core.Commons.Application.Commands.Account;

public static class AccountUpdateCommandFaker
{
    public static Faker<AccountUpdateCommand> Default(Guid id)
    {
        return new Faker<AccountUpdateCommand>()
            .CustomInstantiator(x => new AccountUpdateCommand(
                id,
                x.Person.FullName,
                x.Random.AlphaNumeric(100),
                x.Random.Decimal(-1000, 1000),
                x.Random.Decimal(-1000,1000),
                x.Internet.Color()));
    }

    public static Faker<AccountUpdateCommand> Error(Guid id, string? name = null, string? description = null,
        decimal? openingBalance = null, decimal? overdraft = null, string? color = null)
    {
        return new Faker<AccountUpdateCommand>()
            .CustomInstantiator(faker => new AccountUpdateCommand(
                id,
                name ?? faker.Random.AlphaNumeric(100),
                description ?? faker.Random.AlphaNumeric(100),
                openingBalance ?? faker.Random.Decimal(-1000, 1000),
                overdraft ?? faker.Random.Decimal(-1000, 1000),
                color ?? faker.Internet.Color()
                ));
    }
}