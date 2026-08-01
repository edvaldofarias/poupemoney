using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PoupeMoney.Core.IntegrationTests;

public sealed class HostWebApi : IAsyncDisposable
{
    private IServiceProvider ServiceProvider { get; set; }
    private HttpClient Client { get; set; }
    private IConfiguration? Configuration { get; set; }
    public string ConnectionString => Configuration?.GetConnectionString("") ?? "";
    private const string UrlApi = "http://localhost/apiteste/v1.0/";

    private readonly WebApplicationFactory<Program> _factory;
    private readonly IServiceScope _serviceScope;

    public HostWebApi()
    {
        _factory = new WebApplicationFactory<Program>();
        _factory = _factory.WithWebHostBuilder(builder =>
            builder.UseEnvironment("Test")
            .ConfigureTestServices(services =>
            {
                using var sp = services.BuildServiceProvider();
                var configuration = sp.GetService<IConfiguration>();

                ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Test";
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddJwtBearer("Test", jwt =>
                {
                    jwt.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };

                    var clientId = configuration.GetSection("Firebase:ApiKey").Value;
                    var urlGoogle = clientId ?? throw new NullReferenceException("GoogleClientId is null");
                    var validAudience = configuration.GetValue<string>("Auth:ValidAudience");
                    if (!string.IsNullOrWhiteSpace(validAudience))
                    {
                        jwt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidAudience = validAudience
                        };
                    }
                });
            })
            .ConfigureAppConfiguration((context, configuration) =>
            {
                var caminhoAppSettings = Path.Combine(Directory.GetCurrentDirectory(), "Api", "appsettings.Test.Integration.WebApi.json");
                var caminhoAppSettingsUser = Path.Combine(Directory.GetCurrentDirectory(), "Api", "appsettings.Test.Integration.WebApi.User.json");
                configuration.AddJsonFile(caminhoAppSettings);
                configuration.AddJsonFile(caminhoAppSettingsUser, true);
                configuration.AddUserSecrets<Program>();
                configuration.AddEnvironmentVariables();
            }));

        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = new Uri(UrlApi) });

        _serviceScope = _factory.Services.CreateScope();
        ServiceProvider = _serviceScope.ServiceProvider;
        Configuration = ServiceProvider.GetService<IConfiguration>();
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        if (_serviceScope is IAsyncDisposable asyncDisposableScope)
            await asyncDisposableScope.DisposeAsync();
        else
            _serviceScope.Dispose();

        await _factory.DisposeAsync();
    }
}