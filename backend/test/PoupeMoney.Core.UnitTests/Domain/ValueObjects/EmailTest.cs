namespace PoupeMoney.Core.UnitTests.Domain.ValueObjects;

[Collection("Email Value Object Test")]
public sealed class EmailTest : MainUnitTest
{

    [Fact]
    [Trait("Email", "Create")]
    public void ShouldReturnSuccessWhenEmailIsValid()
    {
        // Act and Arrange
        var address = _faker.Person.Email;
        var email = new Email(address);

        // Assert
        email.ToString().Should().Contain(address);
        email.Address.Should().Contain(address);
    }

    [Theory]
    [Trait("Email", "Create")]
    [InlineData("invalid_email")]
    [InlineData("invalid_email@")]
    [InlineData("invalid_email@domain")]
    [InlineData("invalid_email@domain.")]
    [InlineData("@domain.com")]
    public void ShouldReturnErrorWhenEmailIsInvalid(string email)
    {
        // Arrange and Act
        Action action = () => _ = new Email(email);

        //Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("Email is invalid");
    }

    [Theory]
    [Trait("Email", "Create")]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldReturnErrorWhenEmailIsEmpty(string address)
    {
        // Arrange and Act
        Action action = () => _ = new Email(address);

        //Assert
        action.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'address')");
    }
}