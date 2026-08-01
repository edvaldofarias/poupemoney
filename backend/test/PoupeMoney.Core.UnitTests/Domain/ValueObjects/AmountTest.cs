using System.Globalization;

namespace PoupeMoney.Core.UnitTests.Domain.ValueObjects;

[Collection("Amount Value Object Test")]
public sealed class AmountTest : MainUnitTest
{
    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsValid()
    {
        // Act and Arrange
        var value = _faker.Finance.Amount();
        var amount = new Amount(value);

        // Assert
        amount.ToString().Should().Contain(value.ToString("C", CultureInfo.CurrentCulture));
        amount.Currency.Should().Be(value);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsImplicit()
    {
        // Act and Arrange
        var value = _faker.Finance.Amount();
        Amount amount = value;

        // Assert
        amount.ToString().Should().Contain(value.ToString("C", CultureInfo.CurrentCulture));
        amount.Currency.Should().Be(value);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsImplicitDecimal()
    {
        // Act and Arrange
        var value = _faker.Finance.Amount();
        decimal amount = new Amount(value);

        // Assert
        amount.ToString(CultureInfo.InvariantCulture).Should().Contain(value.ToString(CultureInfo.InvariantCulture));
        amount.Should().Be(value);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsPlus()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 + amount2).ToString().Should().Contain((value1 + value2).ToString("C", CultureInfo.CurrentCulture));
        (amount1 + amount2).Currency.Should().Be(value1 + value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsMinus()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 - amount2).ToString().Should().Contain((value1 - value2).ToString("C", CultureInfo.CurrentCulture));
        (amount1 - amount2).Currency.Should().Be(value1 - value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsMultiply()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 * amount2).ToString().Should().Contain((value1 * value2).ToString("C", CultureInfo.CurrentCulture));
        (amount1 * amount2).Currency.Should().Be(value1 * value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsDivide()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 / amount2).ToString().Should().Contain((value1 / value2).ToString("C", CultureInfo.CurrentCulture));
        (amount1 / amount2).Currency.Should().Be(value1 / value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsMod()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 % amount2).ToString().Should().Contain((value1 % value2).ToString("C", CultureInfo.CurrentCulture));
        (amount1 % amount2).Currency.Should().Be(value1 % value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsEqual()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 == amount2).Should().Be(value1 == value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsNotEqual()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 != amount2).Should().Be(value1 != value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsGreaterThan()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 > amount2).Should().Be(value1 > value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsLessThan()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 < amount2).Should().Be(value1 < value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsGreaterThanOrEqual()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = value1;
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 >= amount2).Should().Be(value1 >= value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsLessThanOrEqual()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = value1;
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        (amount1 <= amount2).Should().Be(value1 <= value2);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsEqualObject()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value1);

        // Assert
        amount1.Equals(amount2).Should().Be(value1.Equals(value1));
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsEqualObjectNull()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);

        // Assert
        amount1.Equals(null).Should().Be(value1.Equals(null));
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsEqualObjectSame()
    {
        // Act and Arrange
        var currency = _faker.Finance.Amount();
        var amount = new Amount(currency);

        // Assert
        amount.Currency.Should().Be(currency);
    }

    [Fact]
    [Trait("Amount", "Create")]
    public void ShouldReturnSuccessWhenAmountIsEqualObjectOther()
    {
        // Act and Arrange
        var value1 = _faker.Finance.Amount();
        var value2 = _faker.Finance.Amount();
        var amount1 = new Amount(value1);
        var amount2 = new Amount(value2);

        // Assert
        amount1.Equals(amount2).Should().Be(value1.Equals(value2));
    }

}