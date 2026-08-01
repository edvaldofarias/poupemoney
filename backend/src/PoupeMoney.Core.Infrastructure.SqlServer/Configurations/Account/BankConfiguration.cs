using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PoupeMoney.Core.Domain.Entities.Account;

namespace PoupeMoney.Core.Infrastructure.SqlServer.Configurations.Account;

[ExcludeFromCodeCoverage]
public sealed class BankConfiguration : IEntityTypeConfiguration<BankEntity>
{
    public void Configure(EntityTypeBuilder<BankEntity> builder)
    {
        builder.ToTable("Banks");

        builder.HasKey(bankEntity => bankEntity.Id);

        builder.Property(bankEntity => bankEntity.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(bankEntity => bankEntity.Code)
            .HasMaxLength(3)
            .IsRequired();
    }
}