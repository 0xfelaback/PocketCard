using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


public class ProjectManagementAPIContextFactory : IDesignTimeDbContextFactory<PocketcardDbContext>
{
    public PocketcardDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PocketcardDbContext>();
        optionsBuilder.UseSqlite("Data Source=/Users/great/Documents/PocketCard/PocketCard.Infrastructure/local.db");

        return new PocketcardDbContext(optionsBuilder.Options);
    }
}