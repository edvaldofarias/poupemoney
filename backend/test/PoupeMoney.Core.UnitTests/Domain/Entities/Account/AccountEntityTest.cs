using PoupeMoney.Core.Commons.Domain.Entities.Account;
using PoupeMoney.Core.Domain.Entities.Account;
using PoupeMoney.Core.Domain.Exceptions;

namespace PoupeMoney.Core.UnitTests.Domain.Entities.Account;

public sealed class AccountEntityTest : MainUnitTest
{
    [Theory(DisplayName = "Criar Conta com parametrôs validos")]
    [InlineData(null)]
    [InlineData("Teste de description")]
    public void CreateAccount_WithValidParameters_ResultObjectValid(string? description)
    {
        //Arrange
        var name = _faker.Name.FullName();
        var openingBalance = new Amount(_faker.Finance.Amount());
        var overdraft = new Amount(_faker.Finance.Amount());
        var color = new Color(_faker.Internet.Color());
        Guid subscriptionId = _faker.Random.Guid();
        Guid bankId = _faker.Random.Guid();

        //Act
        var account = new AccountEntity(name, description, openingBalance, overdraft, color, subscriptionId, bankId);

        //Assert
        account.Should().Match<AccountEntity>((x) =>
            x.Name == name &&
            x.Description == description &&
            x.OpeningBalance == openingBalance &&
            x.Overdraft == overdraft &&
            x.Color == color &&
            x.SubscriptionId == subscriptionId &&
            x.BankId == bankId
        );
    }

    [Theory(DisplayName = "Criar Conta com o parametrô nome invalidos")]
    [InlineData("")]
    [InlineData(
        "Teste com uma palavra com mais de cem caracteres, Teste com uma palavra com mais de cem caracteres!!!")]
    public void CreateAccount_WithNameInvalidParameters_ResultThrowException(string name)
    {
        //Arrange
        var description = _faker.Random.AlphaNumeric(1024);
        var openingBalance = new Amount(_faker.Finance.Amount());
        var overdraft = new Amount(_faker.Finance.Amount());
        var color = new Color(_faker.Internet.Color());
        Guid subscriptionId = _faker.Random.Guid();
        Guid bankId = _faker.Random.Guid();

        //Act
        var action = () =>
            new AccountEntity(name, description, openingBalance, overdraft, color, subscriptionId, bankId);

        //Asserts
        action.Should().Throw<DomainException>();
    }

    [Fact(DisplayName = "Criar Conta com parametro description invalido")]
    public void CreateAccount_WithDescriptionInvalidParameters_ResultThrowException()
    {
        //Arrange
        var name = _faker.Name.FullName();
        var description = _faker.Random.AlphaNumeric(1025);
        var openingBalance = new Amount(_faker.Finance.Amount());
        var overdraft = new Amount(_faker.Finance.Amount());
        var color = new Color(_faker.Internet.Color());
        Guid subscriptionId = _faker.Random.Guid();
        Guid bankId = _faker.Random.Guid();

        //Act
        var action = () =>
            new AccountEntity(name, description, openingBalance, overdraft, color, subscriptionId, bankId);

        //Asserts
        action.Should()
            .Throw<DomainException>()
            .WithMessage($"Invalid Description. too long, maximum 1024 charecters");
    }

    [Fact]
    public void UpdateAccount_WithValidParameters_ResultObjectValid()
    {
        //Arrange
        var account = AccountEntityFaker.Default().Generate();
        var name = _faker.Name.FullName();
        var description = _faker.Random.AlphaNumeric(1024);
        var openingBalance = new Amount(_faker.Finance.Amount());
        var overdraft = new Amount(_faker.Finance.Amount());
        var color = new Color(_faker.Internet.Color());

        //Act
        account.Update(name, description, openingBalance, overdraft, color);

        //Asserts
        account.Should()
            .Match<AccountEntity>(x =>
                x.Name == name &&
                x.Description == description &&
                x.OpeningBalance == openingBalance &&
                x.Overdraft == overdraft &&
                x.Color == color);
    }
}