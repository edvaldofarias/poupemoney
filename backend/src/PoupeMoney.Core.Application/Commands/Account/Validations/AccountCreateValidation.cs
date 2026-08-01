namespace PoupeMoney.Core.Application.Commands.Account.Validations;

public sealed class AccountCreateValidation : AbstractValidator<AccountCreateCommand>
{
    public AccountCreateValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(x => x.OpeningBalance)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Color)
            .NotNull()
            .NotEmpty()
            .MaximumLength(7)
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");

        RuleFor(x => x.BankId)
            .NotNull()
            .NotEmpty();
    }
}
