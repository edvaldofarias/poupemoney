using System.Globalization;
using System.Text.RegularExpressions;

namespace PoupeMoney.Core.Domain.ValueObjects;

public sealed class Email
{
    public Email(string address)
    {
        Address = address;
        if (IsValid() is false)
            throw new ArgumentException("Email is invalid");
    }

    public string Address { get; private init; }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Address))
            throw new ArgumentNullException(nameof(Address));

        try
        {
            Regex.Replace(Address, "(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            static string DomainMapper(Match match)
            {
                var idn = new IdnMapping();
                var domainName = match.Groups[2].Value;
                domainName = idn.GetAscii(domainName);
                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(Address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static implicit operator Email(string address) => new(address);

    public static implicit operator string(Email email) => email.Address;

    public override string ToString() => Address;
}