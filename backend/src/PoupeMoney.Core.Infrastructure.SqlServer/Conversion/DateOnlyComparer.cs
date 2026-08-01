using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

[ExcludeFromCodeCoverage]
public sealed class DateOnlyComparer() : ValueComparer<DateOnly>(
    equalsExpression: (dateOnlyOne, dateOnlyTwo) => dateOnlyOne.DayNumber == dateOnlyTwo.DayNumber,
    hashCodeExpression: dateOnly => dateOnly.GetHashCode());