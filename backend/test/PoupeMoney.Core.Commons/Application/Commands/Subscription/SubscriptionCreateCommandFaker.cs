using PoupeMoney.Core.Application.Commands.Subscription;

namespace PoupeMoney.Core.Commons.Application.Commands.Subscription;

public static class SubscriptionCreateCommandFaker
{
    private static readonly Faker Faker = new("pt_BR");
    private const int MinAge = -18;
    private const int MaxWords = 100;

    public static Faker<SubscriptionCreateCommand> Default()
    {
        return new Faker<SubscriptionCreateCommand>()
            .RuleFor(request => request.DateBirth, DateOnly.FromDateTime(DateTime.Now.AddYears(MinAge)))
            .RuleFor(request => request.Gender, Gender.Other)
            .RuleFor(request => request.Other, faker => faker.Random.AlphaNumeric(MaxWords));
    }

    public static Faker<SubscriptionCreateCommand> Error(DateOnly? dateBirth, string? other)
    {
        return new Faker<SubscriptionCreateCommand>()
            .RuleFor(request => request.DateBirth, dateBirth ?? DateOnly.FromDateTime(DateTime.Now.AddYears(MinAge)))
            .RuleFor(request => request.Gender, Gender.Other)
            .RuleFor(request => request.Other, other ?? (Faker.Random.AlphaNumeric(MaxWords)));
    }
}