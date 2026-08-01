using PoupeMoney.Core.Application.Queries.Account;
using PoupeMoney.Core.Commons.Application.Commands.Account;
using PoupeMoney.Core.Commons.Domain.Entities.Account;
using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.UnitTests.Application.Services;

[Collection("Account Service Test")]
public sealed class AccountServiceTest : MainUnitTest
{
    private readonly Mock<ILogger<AccountService>> _loggerMock = new();
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<ICommonService> _commonServiceMock = new();
    private readonly AccountService _accountService;

    public AccountServiceTest()
    {
        _accountService = new AccountService(
            _loggerMock.Object,
            _accountRepositoryMock.Object,
            _commonServiceMock.Object);
    }

    #region CreateAsync

    [Fact]
    [Trait("Account", "Create")]
    public async Task ShouldReturnSuccessWhenCreateAccountValid()
    {
        // Arrange
        var createCommand = AccountCreateCommandFaker.Default().Generate();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_faker.Random.Guid());
        _accountRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AccountEntity>(), CancellationToken.None));

        // Act
        var response = await _accountService.CreateAsync(createCommand, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Errors.Should().BeEmpty();
    }

    [Theory]
    [Trait("Account", "Create")]
    [InlineData("", "", null, "")]
    [InlineData(" ", " ", "-1", " ")]
    [InlineData("Nubank", null, "0", null)]
    [InlineData(null, "", "0", "teste")]
    public async Task ShouldReturnErrorWhenCreateAccountInvalid(string? name, string? description,
        string? openingBalance, string? color)
    {
        // Arrange
        var createCommand = AccountCreateCommandFaker.Error(name, description, Convert.ToDecimal(openingBalance), color)
            .Generate();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_faker.Random.Guid());
        _accountRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AccountEntity>(), CancellationToken.None));

        // Act
        var response = await _accountService.CreateAsync(createCommand, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().NotBeEmpty();
    }

    [Fact]
    [Trait("Account", "Create")]
    public async Task ShouldReturnErrorWhenCreateAccountWithSubscriptionInvalid()
    {
        // Arrange
        var createCommand = AccountCreateCommandFaker.Default().Generate();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new UnauthorizedAccessException());
        _accountRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AccountEntity>(), CancellationToken.None));

        // Act
        Func<Task> action = async () => await _accountService.CreateAsync(createCommand, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<UnauthorizedAccessException>().ConfigureAwait(true);
    }

    [Fact]
    [Trait("Account", "Create")]
    public async Task ShouldReturnErrorWhenCreateAccountWithSubscriptionNull()
    {
        // Arrange
        var createCommand = AccountCreateCommandFaker.Default().Generate();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new UnauthorizedAccessException());
        _accountRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AccountEntity>(), CancellationToken.None));

        // Act
        Func<Task> action = async () => await _accountService.CreateAsync(createCommand, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    [Trait("Account", "Create")]
    public async Task ShouldReturnErrorWhenCreateAccountWithSubscriptionNullAndAccountInvalid()
    {
        // Arrange
        var createCommand = AccountCreateCommandFaker.Default().Generate();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new UnauthorizedAccessException());
        _accountRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AccountEntity>(), CancellationToken.None));

        // Act
        Func<Task> action = async () => await _accountService.CreateAsync(createCommand, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    #endregion

    #region UpdateAsync

    [Fact]
    [Trait("Account", "Update")]
    public async Task ShouldReturnSuccessWhenUpdateAccountValid()
    {
        //Arrange
        var account = AccountEntityFaker.Default().Generate();
        var command = AccountUpdateCommandFaker.Default(account.Id).Generate();
        _accountRepositoryMock.Setup(x => x.GetByIdAsync(account.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        // Act
        var response = await _accountService.UpdateAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Errors.Should().BeEmpty();
        _accountRepositoryMock.Verify(m => m.UpdateAsync(account, It.IsAny<CancellationToken>()));
    }

    [Theory]
    [Trait("Account", "Update")]
    [InlineData("", "", null, "")]
    [InlineData(" ", " ", "-1", " ")]
    [InlineData("Nubank", null, "0", null)]
    [InlineData(null, "", "0", "teste")]
    public async Task ShouldReturnErrorWhenUpdatedAccountInvalid(string? name, string? description,
        string? openingBalance, string? color)
    {
        // Arrange
        var account = AccountEntityFaker.Default().Generate();
        var command = AccountUpdateCommandFaker
            .Error(account.Id, name, description, Convert.ToDecimal(openingBalance), 0M, color).Generate();
        _accountRepositoryMock.Setup(x => x.GetByIdAsync(account.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        // Act
        var response = await _accountService.UpdateAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().NotBeEmpty();
        _accountRepositoryMock.Verify(
            mock => mock.UpdateAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    [Trait("Account", "Update")]
    public async Task ShouldReturnErrorWhenUpdateAccountWithAccountNotFound()
    {
        //Arrange
        var accountId = Guid.NewGuid();
        var command = AccountUpdateCommandFaker.Default(accountId).Generate();
        _accountRepositoryMock.Setup(x => x.GetByIdAsync(accountId, It.IsAny<CancellationToken>()));

        // Act
        var response = await _accountService.UpdateAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().Contain(x =>
            x.Message.Contains("Account not found", StringComparison.CurrentCultureIgnoreCase));
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    [Trait("Account", "GetById")]
    public async Task ShouldReturnAccountWhenGetAccountExistInBase()
    {
        // Arrange
        var id = Guid.NewGuid();
        var account = AccountEntityFaker.Default().Generate();
        _accountRepositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        // Act
        var response = await _accountService.GetByIdAsync(id, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response!.Id.Should().Be(account.Id);
        response.Name.Should().Be(account.Name);
        response.Description.Should().Be(account.Description);
        response.OpeningBalance.Should().Be(account.OpeningBalance);
        response.Color.Should().Be(account.Color);
        response.SubscriptionId.Should().Be(account.SubscriptionId);
        response.BankId.Should().Be(account.BankId);
        response.Should().BeOfType<AccountQuery>();
    }

    [Fact]
    [Trait("Account", "GetById")]
    public async Task ShouldReturnNullWhenGetAccountNotExistInBase()
    {
        // Arrange
        var id = Guid.NewGuid();
        _accountRepositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AccountEntity?)null);

        // Act
        var response = await _accountService.GetByIdAsync(id, CancellationToken.None);

        // Assert
        response.Should().BeNull();
    }

    #endregion

    #region GetAllAsync

    [Fact]
    [Trait("Account", "GetAll")]
    public async Task ShouldReturnAllAccountsWhenGetAllAccountsExistInBase()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var accounts = AccountEntityFaker.Default().Generate(10);
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(subscriptionId);
        _accountRepositoryMock.Setup(x => x.GetAllBySubscriptionIdAsync(subscriptionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accounts);

        // Act
        var response = await _accountService.GetAllAsync(CancellationToken.None);

        // Assert
        var responses = response.ToList();
        responses.Should().NotBeNullOrEmpty();
        responses.Should().HaveCount(accounts.Count);
        responses.Should().AllBeOfType<AccountQuery>();
    }

    [Fact]
    [Trait("Account", "GetAll")]
    public async Task ShouldReturnEmptyWhenGetAllAccountsNotExistInBase()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        _commonServiceMock.Setup(x => x.GetSubscriptionIdAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(subscriptionId);
        _accountRepositoryMock.Setup(x => x.GetAllBySubscriptionIdAsync(subscriptionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<AccountEntity>());

        // Act
        var response = await _accountService.GetAllAsync(CancellationToken.None);

        // Assert
        response.Should().BeEmpty();
    }

    #endregion
}