namespace PoupeMoney.Core.Application.Commands.Subscription.Validations;
public sealed class SubscriptionCreateValidation : AbstractValidator<SubscriptionCreateCommand>
{
    public SubscriptionCreateValidation()
    {
        RuleFor(x => x.DateBirth)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)));

        RuleFor(x => x.Other)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .When(x => x.Gender == Gender.Other);
    }
}
