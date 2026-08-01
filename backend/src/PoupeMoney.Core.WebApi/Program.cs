using Microsoft.EntityFrameworkCore;

using PoupeMoney.Core.Infrastructure.SqlServer.Context;
using PoupeMoney.Core.WebApi.Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;
var serviceCollection = builder.Services;

//TODO: Organizar isso dentro do Startup
builder.Host.UseSerilog(
    configureLogger: (context, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(context.Configuration));

serviceCollection.AddServices(configuration, environment);

var webApplication = builder.Build();

//TODO: Organizar isso dentro do Startup
using (var scope = webApplication.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PoupeMoneyContext>();
    //await db.Database.MigrateAsync();
}

webApplication.Configure();

webApplication.UseSerilogRequestLogging();

webApplication.Run();


[ExcludeFromCodeCoverage]
public abstract partial class Program;