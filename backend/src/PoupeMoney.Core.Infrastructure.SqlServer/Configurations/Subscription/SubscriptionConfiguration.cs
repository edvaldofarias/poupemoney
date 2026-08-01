using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PoupeMoney.Core.Domain.Entities.Subscription;
using PoupeMoney.Core.Infrastructure.SqlServer.Conversion;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Configurations.Subscription;

[ExcludeFromCodeCoverage]
public sealed class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.ToTable("Subscriptions");

        builder.Property(subscriptionEntity => subscriptionEntity.DateBirth)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>()
            .IsRequired();

        builder.Property(subscriptionEntity => subscriptionEntity.Gender)
            .IsRequired();

        builder.Property(subscriptionEntity => subscriptionEntity.Other)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.OwnsOne(subscription => subscription.Email)
            .Property(email => email.Address)
            .HasColumnName("Email")
            .HasMaxLength(150)
            .IsRequired();
    }
}