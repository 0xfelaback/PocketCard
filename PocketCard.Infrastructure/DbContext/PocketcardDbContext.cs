using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using ProjectManagementAPI.src.Domain;

public class PocketcardDbContext(DbContextOptions<PocketcardDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PocketcardDbContext).Assembly);
    }
}

public static class DbConfig
{
    public static void UseSqlite(this IServiceCollection services, string connectionString = null)
    {
        var defaultConnectionString = "Data Source=/app/data/pocketcard.db";
        var actualConnectionString = connectionString ?? defaultConnectionString;

        services.AddDbContext<PocketcardDbContext>(options => options.UseSqlite(actualConnectionString));
    }
}