using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace PoupeMoney.Core.IntegrationTests;

public sealed class HostWebApi(string environment = "Test") : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(environment);
        builder.UseSetting("WarmUp", "false");
        builder.ConfigureAppConfiguration((_, configuration) =>
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["WarmUp"] = "false",
                ["Firebase:ApiKey"] = "integration-test",
                ["ConnectionStrings:DefaultConnection"] = "Server=localhost;Database=integration;User Id=sa;Password=integration-test;TrustServerCertificate=True;"
            }));
    }
}