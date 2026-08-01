using PoupeMoney.Core.Application.Queries.Account;
using PoupeMoney.Core.Commons.Application.Commands.Account;
using PoupeMoney.Core.Commons.Domain.Entities.Account;

namespace PoupeMoney.Core.UnitTests.WebApi.Controllers.V1;

public sealed class AccountControllerTest : MainUnitTest
{
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly AccountController _accountController;
    private readonly CancellationToken _cancellationToken = It.IsAny<CancellationToken>();

    public AccountControllerTest()
    {
        var loggerMock = new Mock<ILogger<AccountController>>();
        _accountServiceMock = new Mock<IAccountService>();
        _accountController = new AccountController(loggerMock.Object, _accountServiceMock.Object);
    }

    #region Get All

    //TODO: Melhorar validação do teste
    [Fact]
    public async Task ShouldReturnAccountWhenAccountExistsInDbAsync()
    {
        // Arrange
        var accounts = AccountEntityFaker.Default().Generate(10);
        var accountQueries = accounts.Select(x =>
            new AccountQuery(x.Id, x.Name, x.Description, x.OpeningBalance, x.Color, x.SubscriptionId, x.BankId));
        _accountServiceMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(accountQueries);

        // Act
        var result = await _accountController.Get(_cancellationToken);

        // Asserts
        var response = result.Result;
        response.Should().BeOfType<OkObjectResult>();
    }

    #endregion

    #region Get By Id

    [Fact]
    public async Task ShouldReturnAccountByIdWhenAccountExistsInDbAsync()
    {
        // Arrange
        var account = AccountEntityFaker.Default().Generate();
        var accountQuery = new AccountQuery(account.Id, account.Name, account.Description, account.OpeningBalance,
            account.Color, account.SubscriptionId, account.BankId);
        _accountServiceMock.Setup(x => x.GetByIdAsync(account.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accountQuery);

        // Act
        var result = await _accountController.GetById(account.Id, _cancellationToken);

        // Asserts
        var response = result.Result;
        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenAccountNotExistsInDbAsync()
    {
        // Arrange
        // Act
        var result = await _accountController.GetById(It.IsAny<Guid>(), _cancellationToken);

        // Asserts
        var response = result.Result;
        response.Should().BeOfType<NotFoundResult>();
    }

    #endregion

    #region Post

    [Fact]
    public async Task ShouldReturnCreatedWhenAccountCreatedInDbAsync()
    {
        // Arrange
        var id = Guid.Empty;
        var accountCreateCommand = AccountCreateCommandFaker.Default().Generate();
        var response = new Response<Guid>();
        _accountServiceMock.Setup(x => x.CreateAsync(accountCreateCommand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = (ObjectResult) await _accountController.Post(accountCreateCommand);

        // Asserts
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task ShouldReturnUnprocessableEntityObjectResultWhenAccountNotCreatedInDbAsync()
    {
        // Arrange
        var errors = new ValidationFailure("Test", "Test is errors");
        var accountCreateCommand = AccountCreateCommandFaker.Default().Generate();
        var response = new Response<Guid>();
        response.AddError(errors);
        _accountServiceMock.Setup(x => x.CreateAsync(accountCreateCommand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = (ObjectResult)await _accountController.Post(accountCreateCommand);

        // Asserts
        result.Should().BeOfType<UnprocessableEntityObjectResult>();
        //TODO: Verificar como validar o retorno da mensagem
    }

    #endregion

    #region Put

    [Fact]
    public async Task ShouldReturnNoContentWhenAccountUpdatedInDbAsync()
    {
        // Arrange
        var id = Guid.Empty;
        var accountUpdateCommand = AccountUpdateCommandFaker.Default(id).Generate();
        var response = new Response();
        _accountServiceMock.Setup(x => x.UpdateAsync(accountUpdateCommand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _accountController.Put(accountUpdateCommand);

        // Asserts
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task ShouldReturnUnprocessableEntityObjectResultWhenAccountNotUpdatedInDbAsync()
    {
        // Arrange
        var id = Guid.Empty;
        var errors = new ValidationFailure("Test", "Test is errors");
        var accountUpdateCommand = AccountUpdateCommandFaker.Default(id).Generate();
        var response = new Response();
        response.AddError(errors);
        _accountServiceMock.Setup(x => x.UpdateAsync(accountUpdateCommand, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = (ObjectResult)await _accountController.Put(accountUpdateCommand);

        // Asserts
        result.Should().BeOfType<UnprocessableEntityObjectResult>();
    }

    #endregion
}