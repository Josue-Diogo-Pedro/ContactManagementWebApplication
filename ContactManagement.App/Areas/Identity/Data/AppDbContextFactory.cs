using ContactManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactManagement.App.Areas.Identity.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<ManagementAppContext>
{
    public ManagementAppContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

        var builder = new DbContextOptionsBuilder<ManagementAppContext>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseSqlServer(connectionString);

        return new ManagementAppContext(builder.Options);
    }
}
