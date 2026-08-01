using PoupeMoney.Core.Application.Commands.Account;

namespace PoupeMoney.Core.Commons.Application.Commands.Account;

public static class AccountCreateCommandFaker
{
    public static Faker<AccountCreateCommand> Default()
    {
        return new Faker<AccountCreateCommand>()
            .RuleFor(request => request.Name, faker => faker.Person.FullName)
            .RuleFor(request => request.Description, faker => faker.Random.AlphaNumeric(100))
            .RuleFor(request => request.OpeningBalance, faker => faker.Random.Decimal(-1000, 1000))
            .RuleFor(request => request.Color, faker => faker.Internet.Color())
            .RuleFor(request => request.BankId, faker => faker.Random.Guid())
            .RuleFor(request => request.Overdraft, faker => faker.Random.Decimal(-1000, 1000));
    }

    public static Faker<AccountCreateCommand> Error(
        string? name = null,
        string? description = null,
        decimal? openingBalance = null,
        string? color = null,
        Guid? bankId = null,
        decimal? overdraft = null)
    {
        return new Faker<AccountCreateCommand>()
            .RuleFor(request => request.Name, faker => name ?? faker.Random.AlphaNumeric(100))
            .RuleFor(request => request.Description, faker => description ?? faker.Random.AlphaNumeric(100))
            .RuleFor(request => request.OpeningBalance, faker => openingBalance ?? faker.Random.Decimal(-1000, 1000))
            .RuleFor(request => request.Color, faker => color ?? faker.Internet.Color())
            .RuleFor(request => request.BankId, faker => bankId ?? faker.Random.Guid())
            .RuleFor(request => request.Overdraft, faker => overdraft ?? faker.Random.Decimal(-1000, 1000));
    }
}