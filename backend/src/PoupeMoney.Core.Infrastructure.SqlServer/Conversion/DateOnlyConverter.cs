using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

[ExcludeFromCodeCoverage]
public sealed class DateOnlyConverter() : ValueConverter<DateOnly, DateTime>(
    convertToProviderExpression: dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
    convertFromProviderExpression: dateTime => DateOnly.FromDateTime(dateTime));