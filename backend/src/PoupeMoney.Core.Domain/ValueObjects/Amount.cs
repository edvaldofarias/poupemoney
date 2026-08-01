using System.Globalization;

namespace PoupeMoney.Core.Domain.ValueObjects;

public sealed class Amount(decimal currency)
{
    public decimal Currency { get; private init; } = currency;

    public static implicit operator Amount(decimal currency) => new(currency);

    public static implicit operator decimal(Amount amount) => amount.Currency;

    public static Amount operator +(Amount amount1, Amount amount2) => new(amount1.Currency + amount2.Currency);

    public static Amount operator -(Amount amount1, Amount amount2) => new(amount1.Currency - amount2.Currency);

    public static Amount operator *(Amount amount1, Amount amount2) => new(amount1.Currency * amount2.Currency);

    public static Amount operator /(Amount amount1, Amount amount2) => new(amount1.Currency / amount2.Currency);

    public static Amount operator %(Amount amount1, Amount amount2) => new(amount1.Currency % amount2.Currency);

    public static bool operator ==(Amount amount1, Amount amount2) => amount1.Currency == amount2.Currency;

    public static bool operator !=(Amount amount1, Amount amount2) => amount1.Currency != amount2.Currency;

    public static bool operator >(Amount amount1, Amount amount2) => amount1.Currency > amount2.Currency;

    public static bool operator <(Amount amount1, Amount amount2) => amount1.Currency < amount2.Currency;

    public static bool operator >=(Amount amount1, Amount amount2) => amount1.Currency >= amount2.Currency;

    public static bool operator <=(Amount amount1, Amount amount2) => amount1.Currency <= amount2.Currency;

    public override bool Equals(object? obj)
    {
        return obj is Amount amount && Currency == amount.Currency;
    }

    public override int GetHashCode() => Currency.GetHashCode();

    public override string ToString() => Currency.ToString("C", CultureInfo.CurrentCulture);
}