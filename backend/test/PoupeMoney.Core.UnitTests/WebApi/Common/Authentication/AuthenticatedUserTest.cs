using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using PoupeMoney.Core.WebApi.Common.Authentication;

namespace PoupeMoney.Core.UnitTests.WebApi.Common.Authentication;

public sealed class AuthenticatedUserTest : MainUnitTest
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();

    private AuthenticatedUser _authenticatedUser = default!;

    [Fact]
    public void ShouldReturnTrueWhenUserIsAuthenticated()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, _faker.Person.Email)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        _authenticatedUser = new AuthenticatedUser(_httpContextAccessorMock.Object);
        var result = _authenticatedUser.IsAuthenticated();

        // Asserts
        result.Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnFalseWhenUserIsNotAuthenticated()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        _authenticatedUser = new AuthenticatedUser(_httpContextAccessorMock.Object);
        var result = _authenticatedUser.IsAuthenticated();

        // Asserts
        result.Should().BeFalse();
    }

    [Fact]
    public void ShouldReturnTrueWhenUserIsAuthenticatedAndIdAndEmailIsNotNull()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, _faker.Person.Email)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        _authenticatedUser = new AuthenticatedUser(_httpContextAccessorMock.Object);
        var result = _authenticatedUser.IsAuthenticated();

        // Asserts
        result.Should().BeTrue();
        _authenticatedUser.Id.Should().NotBeNullOrEmpty();
        _authenticatedUser.Email.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldReturnFalseWhenUserIsAuthenticatedAndIdAndEmailIsNull()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, _faker.Person.Email)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        _authenticatedUser = new AuthenticatedUser(_httpContextAccessorMock.Object);
        var result = _authenticatedUser.IsAuthenticated();

        // Asserts
        result.Should().BeTrue();
        _authenticatedUser.Id.Should().NotBeNullOrEmpty();
        _authenticatedUser.Email.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldReturnExceptionWhenNameIdentifierNullOrEmpty()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, string.Empty),
            new(ClaimTypes.Email, _faker.Person.Email)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        Action action = () =>
        {
            _ = new AuthenticatedUser(_httpContextAccessorMock.Object);
        };

        // Asserts
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldReturnExceptionWhenEmailNullOrEmpty()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, string.Empty)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        Action action = () =>
        {
            _ = new AuthenticatedUser(_httpContextAccessorMock.Object);
        };

        // Asserts
        action.Should().Throw<ArgumentException>();
    }
}