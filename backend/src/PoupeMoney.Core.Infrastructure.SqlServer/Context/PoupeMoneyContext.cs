using PoupeMoney.Core.Domain.Entities.Account;
using PoupeMoney.Core.Domain.Entities.Subscription;
using PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Context;

[ExcludeFromCodeCoverage]
public sealed class PoupeMoneyContext(DbContextOptions<PoupeMoneyContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>();

        configurationBuilder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter, TimeOnlyComparer>();

        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 3)
            .HaveColumnType("decimal(18,3)")
            .HaveConversion<decimal>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoupeMoneyContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<SubscriptionEntity> Subscription => Set<SubscriptionEntity>();

    public DbSet<AccountEntity> Account => Set<AccountEntity>();

    public DbSet<BankEntity> Bank => Set<BankEntity>();
}
