using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

[ExcludeFromCodeCoverage]
public sealed class TimeOnlyComparer() : ValueComparer<TimeOnly>(
    equalsExpression: (timeOnlyOne, timeOnlyTwo) => timeOnlyOne.Ticks == timeOnlyTwo.Ticks,
    hashCodeExpression: timeOnly => timeOnly.GetHashCode());