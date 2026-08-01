namespace PoupeMoney.Core.Application.Commons;

public record Error(string Message, string Property, string? AttemptedValue = null);