using PoupeMoney.Core.Application.Commands.Subscription;

namespace PoupeMoney.Core.UnitTests.WebApi.Controllers.V1;

public sealed class SubscriptionsControllerTest : MainUnitTest
{
    private readonly Mock<ISubscriptionService> _subscriptionServiceMock;
    private readonly SubscriptionController _subscriptionController;
    private readonly CancellationToken _cancellationToken = It.IsAny<CancellationToken>();

    public SubscriptionsControllerTest()
    {
        _subscriptionServiceMock = new Mock<ISubscriptionService>();
        Mock<ILogger<SubscriptionController>> loggerMock = new();
        _subscriptionController = new SubscriptionController(_subscriptionServiceMock.Object, loggerMock.Object);
    }

    #region Post - Create Subscription

    [Fact]
    [Trait("Subscription", "Create")]
    public async Task ReturnSuccessWhenSubscriptionIsValidAsync()
    {
        // Arrange
        var command = new SubscriptionCreateCommand();
        var responde = new Response();
        _subscriptionServiceMock
            .Setup(x => x.CreateAsync(command, CancellationToken.None))
            .ReturnsAsync(responde);

        // Act
        var result = await _subscriptionController.Post(command, _cancellationToken);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    [Trait("Subscription", "Create")]
    public async Task ReturnUnprocessableEntityObjectResultWhenSubscriptionIsInvalidAsync()
    {
        // Arrange
        var command = new SubscriptionCreateCommand();
        var errors = new List<ValidationFailure> {new("DateBirth", "DateBirth is invalid")};
        var response = new Response();
        response.AddError(errors);
        _subscriptionServiceMock
            .Setup(x => x.CreateAsync(command, CancellationToken.None))
            .ReturnsAsync(response);

        // Act
        var result = (ObjectResult)await _subscriptionController.Post(command, _cancellationToken);

        // Assert
        result.Should().BeOfType<UnprocessableEntityObjectResult>();
        result.Value.Should()
            .BeEquivalentTo(response.Errors);
    }

    #endregion

    #region Get - Get Subscription

    [Fact]
    [Trait("Subscription", "Get")]
    public void ReturnNotImplementationStatusWhenGetSubscriptionIsValidAsync()
    {
        // Arrange

        // Act
        var result = (StatusCodeResult)_subscriptionController.Get(_cancellationToken);

        // Assert
        result.StatusCode.Should().Be(501);
    }

    #endregion
}