using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Commons.Application.Commands.Subscription;
using PoupeMoney.Core.Domain.Entities.Subscription;

namespace PoupeMoney.Core.UnitTests.Application.Services;

[Collection("Subscription Service Test")]
public sealed class SubscriptionServiceTest : MainUnitTest
{
    private readonly Mock<IAccountService> _accountService = new();
    private readonly Mock<ISubscriptionRepository> _subscriptionRepository = new();
    private readonly Mock<IAuthenticatedUser> _authenticatedUser = new();
    private readonly Mock<ILogger<SubscriptionService>> _logger = new();
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionServiceTest()
    {
        _subscriptionService = new SubscriptionService(
            _authenticatedUser.Object,
            _accountService.Object,
            _subscriptionRepository.Object,
            _logger.Object);
    }

    #region CreateAsync

    [Fact]
    [Trait("Subscription", "Create")]
    public async Task ShouldReturnsWhenSubscriptionCreatedWithSuccess()
    {
        // Arrange
        var request = SubscriptionCreateCommandFaker.Default().Generate();
        _authenticatedUser.Setup(x => x.Email).Returns(_faker.Person.Email);
        _authenticatedUser.Setup(x => x.Id).Returns(_faker.Random.AlphaNumeric(10));
        _subscriptionRepository.Setup(x => x.CreateAsync(It.IsAny<SubscriptionEntity>(), CancellationToken.None));
        _accountService.Setup(x => x.CreateAsync(It.IsAny<AccountCreateCommand>(), CancellationToken.None));
        // Act
        var response = await _subscriptionService.CreateAsync(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Errors.Should().BeEmpty();
    }

    [Fact]
    [Trait("Subscription", "Create")]
    public async Task ShouldReturnsWhenSubscriptionCreatedWithError()
    {
        // Arrange
        var dateBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(-17));
        var otherGender = _faker.Random.AlphaNumeric(150);
        var request = SubscriptionCreateCommandFaker.Error(dateBirth, otherGender).Generate();
        _authenticatedUser.Setup(x => x.Email).Returns(_faker.Person.Email);
        _authenticatedUser.Setup(x => x.Id).Returns(_faker.Random.AlphaNumeric(10));
        _subscriptionRepository.Setup(x => x.CreateAsync(It.IsAny<SubscriptionEntity>(), CancellationToken.None));

        // Act
        var response = await _subscriptionService.CreateAsync(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().HaveCount(2);
        response.Errors.Should().Contain(x => x.Property.Contains("DateBirth"));
        response.Errors.Should().Contain(x => x.Property.Contains("Other"));
    }

    #endregion
}