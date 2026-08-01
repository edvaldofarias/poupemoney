using PoupeMoney.Core.Application.Queries.Bank;
using PoupeMoney.Core.Commons.Domain.Entities.Account;

namespace PoupeMoney.Core.UnitTests.WebApi.Controllers.V1;

public sealed class BankControllerTest : MainUnitTest
{
    private readonly Mock<IBankService> _bankServiceMock;
    private readonly BankController _bankController;
    private readonly CancellationToken _cancellationToken = It.IsAny<CancellationToken>();

    public BankControllerTest()
    {
        Mock<ILogger<BankController>> loggerMock = new();
        _bankServiceMock = new Mock<IBankService>();
        _bankController = new BankController(loggerMock.Object, _bankServiceMock.Object);
    }

    #region Get All


    //TODO: Melhorar os testes
    [Fact]
    public async Task ShouldReturnListBankWhenExistsBankInDbAsync()
    {
        // Arrange
        var banks = BankEntityFaker.Default().Generate(10);
        var bankQueries = banks.Select(x => new BankQuery(x.Id, x.Name, x.Code));
        _bankServiceMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bankQueries);

        // Act
        var result = await _bankController.Get(_cancellationToken);

        // Asserts
        var response = result.Result;

        response.Should().BeOfType<OkObjectResult>();
        // response..Should().HaveCount(10);
    }

    #endregion

    #region Get By Id

    //TODO: Melhorar e validar o teste
    [Fact]
    public async Task ShouldReturnBankByIdWhenExistsBankInDbAsync()
    {
        // Arrange
        var bank = BankEntityFaker.Default().Generate();
        var bankQuery = new BankQuery(bank.Id, bank.Name, bank.Code);
        _bankServiceMock.Setup(x => x.GetByIdAsync(bankQuery.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bankQuery);

        // Act
        var result = await _bankController.GetById(bank.Id, _cancellationToken);

        // Asserts
        var response = result.Result;
        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenBankNotExistsInDbAsync()
    {
        // Arrange

        // Act
        var result = await _bankController.GetById(It.IsAny<Guid>(), _cancellationToken);

        // Asserts
        var response = result.Result;
        response.Should().BeOfType<NotFoundResult>();
    }

    #endregion
}