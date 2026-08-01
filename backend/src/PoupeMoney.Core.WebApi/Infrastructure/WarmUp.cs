using System.Diagnostics;

using PoupeMoney.Core.Infrastructure.SqlServer.Context;

namespace PoupeMoney.Core.WebApi.Infrastructure;

[ExcludeFromCodeCoverage]
public sealed class WarmUp(
    IServiceProvider serviceProvider,
    ILogger<WarmUp> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var scopeServiceProvider = scope.ServiceProvider;

        await WarmUpWebApiAsync(scopeServiceProvider, stoppingToken);
        await WarmUpDatabaseAsync(scopeServiceProvider, stoppingToken);
    }

    private async Task WarmUpWebApiAsync(IServiceProvider scopeServiceProvider, CancellationToken stoppingToken)
    {
        await MeasureAsync("making warm up request", async () =>
        {
            try
            {
                var serverUrls = scopeServiceProvider.GetRequiredService<IConfiguration>()[WebHostDefaults.ServerUrlsKey];
                if (string.IsNullOrWhiteSpace(serverUrls))
                {
                    logger.LogWarning("Could not detect server url, no warm up taking place.");
                }
                else
                {
                    var uri = GetUri(serverUrls);
                    logger.LogDebug("Warming up at {fullUri}", uri);
                    await IsWarmUpRequestAsync(uri, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error on warm up request - {@Exception}", ex);
                throw;
            }
        });
    }

    private async Task WarmUpDatabaseAsync(IServiceProvider scopeServiceProvider, CancellationToken stoppingToken)
    {
        await MeasureAsync("making warm up database", async () =>
        {
            try
            {
                var context = scopeServiceProvider.GetRequiredService<PoupeMoneyContext>();
                var isConnecting = await context.Database.CanConnectAsync(stoppingToken);
                if (isConnecting)
                    logger.LogDebug("Connect to database with succeed");
                else
                    logger.LogWarning("Database is not connection");
            }
            catch (Exception ex)
            {
                logger.LogError("Error on connect to database - {@Exception}", ex);
                throw;
            }
        });
    }

    private static Uri GetUri(string serverUrls)
    {
        var urls = serverUrls.Split(';').Select(address => new Uri(address)).ToList();
        var url = urls.FirstOrDefault(url => url is {IsLoopback: true, Scheme: "https"})?.ToString()
                  ?? urls.First().ToString();
        url = url.EndsWith('/') ? url[..^1] : url;
        var fullUri = $"{url}/v1/application/warmup/";
        return new Uri(fullUri);
    }

    private async Task IsWarmUpRequestAsync(Uri uri, CancellationToken stoppingToken)
    {
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        using var httpClient = httpClientFactory.CreateClient("WarmUp");
        using var response = await httpClient.GetAsync(uri, stoppingToken);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(stoppingToken);
            var message = string.IsNullOrWhiteSpace(content) ? "No response content was sent." : $"Response was \n{content}";
            logger.LogError("Warm up request failed with status code {StatusCode}. {Content}", response.StatusCode, message);

        }
        logger.LogDebug("Warm up request succeeded with status code {StatusCode}.", response.StatusCode);
    }


    private async ValueTask MeasureAsync(string measurement, Func<Task> action)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await action();
        stopwatch.Stop();
        logger.LogDebug("Took {ElapsedMilliseconds}ms on {measurement}.", stopwatch.ElapsedMilliseconds, measurement);
    }

    private void Measure(string measurement, Action action)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        action();
        stopwatch.Stop();
        logger.LogDebug("Took {ElapsedMilliseconds}ms {measurement}.", stopwatch.ElapsedMilliseconds, measurement);
    }
}