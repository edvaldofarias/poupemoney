using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

[ExcludeFromCodeCoverage]
public sealed class TimeOnlyConverter() : ValueConverter<TimeOnly, TimeSpan>(
    convertToProviderExpression: timeOnly => timeOnly.ToTimeSpan(),
    convertFromProviderExpression: timeSpan => TimeOnly.FromTimeSpan(timeSpan));