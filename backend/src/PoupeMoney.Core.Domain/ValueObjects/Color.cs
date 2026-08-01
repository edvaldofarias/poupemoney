using System.Text.RegularExpressions;

namespace PoupeMoney.Core.Domain.ValueObjects;

public sealed class Color
{
    public string Value { get; private init; }

    public Color(string value)
    {
        Value = value;

        if (IsValid()) return;

        throw new ArgumentException($"{Value} is Invalid");
    }

    private bool IsValid()
    {
        const string rgbValidation = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
        var match = Regex.Match(Value, rgbValidation, RegexOptions.IgnoreCase);

        return match.Success;
    }

    public static implicit operator Color(string value) => new(value);

    public static implicit operator string(Color color) => color.Value;

    public override bool Equals(object? obj)
    {
        return obj is Color color && Value == color.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}