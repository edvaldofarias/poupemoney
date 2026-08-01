using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Commons.Domain.Entities.Account;

public static class BankEntityFaker
{
    public static Faker<BankEntity> Default()
    {
        return new Faker<BankEntity>()
            .UsePrivateConstructor()
            .RuleFor(x => x.Name, faker => faker.Company.CompanyName())
            .RuleFor(x => x.Code, faker => faker.Random.Number(0, 999));
    }
}
