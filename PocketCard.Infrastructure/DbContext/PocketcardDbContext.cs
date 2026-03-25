using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class PocketcardDbContext(DbContextOptions<PocketcardDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PocketcardDbContext).Assembly);
    }
}

public static class DbConfig
{
    public static void UseSqlite(this IServiceCollection services, string connectionString = "Data Source=/Users/great/Desktop/cs/PocketCard/PocketCard.Infrastructure/local.db")
    {
        var defaultConnectionString = "Data Source=/app/data/pocketcard.db";
        var actualConnectionString = connectionString ?? defaultConnectionString;

        services.AddDbContext<PocketcardDbContext>(options => options.UseSqlite(actualConnectionString));
    }
}