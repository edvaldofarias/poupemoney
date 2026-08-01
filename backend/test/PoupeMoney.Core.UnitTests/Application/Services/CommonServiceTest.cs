using PoupeMoney.Core.Commons.Domain.Entities.Subscription;
using PoupeMoney.Core.Domain.Entities.Subscription;

namespace PoupeMoney.Core.UnitTests.Application.Services;

[Collection("Common Service Test")]
public sealed class CommonServiceTest : MainUnitTest
{
    private readonly Mock<IAuthenticatedUser> _authenticatedUserMock = new();
    private readonly Mock<ISubscriptionRepository> _subscriptionRepositoryMock = new();
    private readonly Mock<ILogger<CommonService>> _loggerMock = new();
    private readonly ICommonService _commonService;

    public CommonServiceTest()
    {
        _commonService = new CommonService(
            _authenticatedUserMock.Object,
            _subscriptionRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    [Trait("Common", "GetByIdSubscription")]
    public async Task ShouldReturnSuccessWhenSubscriptionExistsAsync()
    {
        // Arrange
        var subscription = SubscriptionEntityFaker.Default().Generate();
        _subscriptionRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(subscription);

        // Act
        var result = await _commonService.GetSubscriptionIdAsync(It.IsAny<CancellationToken>());

        // Assert
        result.Should().Be(subscription.Id);
    }

    [Fact]
    [Trait("Common", "GetByIdSubscription")]
    public async Task ShouldReturnExceptionWhenSubscriptionNotFoundAsync()
    {
        // Arrange
        const string exceptionMessage = "Subscription not found";
        _subscriptionRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<SubscriptionEntity?>(null));

        // Act
        var action = async () => await _commonService.GetSubscriptionIdAsync(It.IsAny<CancellationToken>());

        // Assert
        await action.Should()
            .ThrowAsync<UnauthorizedAccessException>()
            .WithMessage(exceptionMessage);
    }
}