using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FarmManager.Model.DatabaseContext;

public class FarmManagerContextFactory : IDesignTimeDbContextFactory<FarmManagerContext>
{
    public FarmManagerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FarmManagerContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=FarmManager;Username=postgres;Password=admin");

        return new FarmManagerContext(optionsBuilder.Options);
    }
}
