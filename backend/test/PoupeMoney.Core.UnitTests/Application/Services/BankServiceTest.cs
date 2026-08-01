using PoupeMoney.Core.Application.Queries.Bank;
using PoupeMoney.Core.Commons.Domain.Entities.Account;
using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.UnitTests.Application.Services;

[Collection("Bank Service Test")]
public sealed class BankServiceTest : MainUnitTest
{
    private readonly Mock<IBankRepository> _bankRepositoryMock = new();
    private readonly IBankService _bankService;
    public BankServiceTest()
    {
        _bankService = new BankService(_bankRepositoryMock.Object);
    }

    [Fact]
    [Trait("Bank", "GetById")]
    public async Task ShouldReturnBankWhenBankExistsInDatabaseAsync()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = It.IsAny<CancellationToken>();
        var bank = BankEntityFaker.Default().Generate();
        _bankRepositoryMock.Setup(x => x.GetByIdAsync(id, cancellationToken))
            .ReturnsAsync(bank);

        // Act
        var result = await _bankService.GetByIdAsync(id, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(bank.Id);
        result.Name.Should().Be(bank.Name);
        result.Code.Should().Be(bank.Code);
        result.Should().BeOfType<BankQuery>();
    }

    [Fact]
    [Trait("Bank", "GetById")]
    public async Task ShouldReturnNullWhenBankNotFoundAsync()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = It.IsAny<CancellationToken>();
        _bankRepositoryMock.Setup(x => x.GetByIdAsync(id, cancellationToken))
            .Returns(Task.FromResult<BankEntity?>(null));

        // Act
        var result = await _bankService.GetByIdAsync(id, cancellationToken);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    [Trait("Bank", "GetAll")]
    public async Task ShouldReturnBanksWhenBanksExistsInDatabaseAsync()
    {
        // Arrange
        var cancellationToken = It.IsAny<CancellationToken>();
        var banks = BankEntityFaker.Default().Generate(10);
        _bankRepositoryMock.Setup(x => x.GetAllAsync(cancellationToken))
            .ReturnsAsync(banks);

        // Act
        var result = (await _bankService.GetAllAsync(cancellationToken)).ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(banks.Count);
        result.Should().AllBeOfType<BankQuery>();
    }

    [Fact]
    [Trait("Bank", "GetAll")]
    public async Task ShouldReturnEmptyWhenBanksNotFoundAsync()
    {
        // Arrange
        var cancellationToken = It.IsAny<CancellationToken>();
        _bankRepositoryMock.Setup(x => x.GetAllAsync(cancellationToken))
            .ReturnsAsync(Enumerable.Empty<BankEntity>());

        // Act
        var result = (await _bankService.GetAllAsync(cancellationToken)).ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(0);
        result.Should().AllBeOfType<BankQuery>();
    }
}