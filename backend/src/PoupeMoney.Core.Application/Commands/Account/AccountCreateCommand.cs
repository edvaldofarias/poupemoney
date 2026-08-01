namespace PoupeMoney.Core.Application.Commands.Account;

public sealed class AccountCreateCommand(
    string name,
    string? description,
    decimal openingBalance,
    decimal overdraft,
    string color,
    Guid bankId)
{
    public AccountCreateCommand() :
        this("Carteira", "Conta padrão", 0, 0, "#000000", Guid.Empty)
    {
    }

    public string Name { get; init; } = name;
    public string? Description { get; init; } = description;
    public decimal OpeningBalance { get; init; } = openingBalance;
    public decimal Overdraft { get; init; } = overdraft;
    public string Color { get; init; } = color;
    public Guid BankId { get; init; } = bankId;
}
