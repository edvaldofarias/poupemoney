using System.Net;

namespace PoupeMoney.Core.IntegrationTests;

public sealed class ApplicationEndpointTests
{
    [Fact]
    public async Task GetApplication_WhenApiStarts_ReturnsOk()
    {
        await using var factory = new HostWebApi();
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/v1/application", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAccount_WithoutToken_ReturnsUnauthorized()
    {
        await using var factory = new HostWebApi();
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/v1/account", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSwagger_InDevelopment_ReturnsDocument()
    {
        await using var factory = new HostWebApi("Development");
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/swagger/v1/swagger.json", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}