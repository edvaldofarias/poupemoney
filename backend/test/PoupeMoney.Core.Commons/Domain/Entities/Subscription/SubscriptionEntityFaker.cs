using PoupeMoney.Core.Domain.Entities.Subscription;
using PoupeMoney.Core.Domain.ValueObjects;

namespace PoupeMoney.Core.Commons.Domain.Entities.Subscription;

public static class SubscriptionEntityFaker
{
    private static readonly Faker Faker = new("pt_BR");
    public static Faker<SubscriptionEntity> Default()
    {
        return new Faker<SubscriptionEntity>()
            .UsePrivateConstructor()
            .RuleFor(property => property.DateBirth, DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
            .RuleFor(property => property.Email, faker => new Email(faker.Person.Email))
            .RuleFor(property => property.Gender, Gender.Female)
            .RuleFor(property => property.Other, faker => faker.Random.AlphaNumeric(100));
    }
}