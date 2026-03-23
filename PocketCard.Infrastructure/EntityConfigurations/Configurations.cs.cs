using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PocketCard.Infrastructure;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => new { u.Name, u.Email }).IsUnique();
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.AccountID).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Type);
        builder.Property(c => c.CardNumber).IsRequired();
        builder.Property(c => c.Contactless);
        builder.Property(c => c.CVV).IsRequired();
        builder.Property(c => c.ExpirationDate).IsRequired();
        builder.Property(c => c.InternationalUsage);
        builder.Property(c => c.Network);
    }
}
