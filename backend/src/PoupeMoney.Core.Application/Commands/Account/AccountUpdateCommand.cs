namespace PoupeMoney.Core.Application.Commands.Account;

public record AccountUpdateCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal OpeningBalance,
    decimal Overdraft,
    string Color);
