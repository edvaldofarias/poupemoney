using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Configurations.Account;

[ExcludeFromCodeCoverage]
public sealed class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder
            .ToTable("Accounts")
            .HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(accountEntity => accountEntity.Description)
            .HasMaxLength(1024)
            .IsRequired(false);

        builder.Property(accountEntity => accountEntity.SubscriptionId)
            .IsRequired();

        builder.HasOne(accountEntity => accountEntity.Subscription)
            .WithMany(subscriptionEntity => subscriptionEntity.Accounts)
            .HasForeignKey(accountEntity => accountEntity.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(accountEntity => accountEntity.Bank)
            .WithMany(bankEntity => bankEntity.Accounts)
            .HasForeignKey(accountEntity => accountEntity.BankId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(account => account.OpeningBalance)
            .Property(openingBalance => openingBalance.Currency)
            .HasColumnName("OpeningBalance")
            .IsRequired();

        builder.OwnsOne(account => account.Overdraft)
            .Property(overdraft => overdraft.Currency)
            .HasColumnName("Overdraft")
            .IsRequired();

        builder.OwnsOne(account => account.Color)
            .Property(color => color.Value)
            .HasColumnName("Color")
            .IsRequired();
    }
}