namespace PoupeMoney.Core.Application.Queries.Account;

public record AccountQuery(
    Guid Id,
    string Name, 
    string? Description, 
    decimal OpeningBalance, 
    string Color, 
    Guid SubscriptionId, 
    Guid BankId);
