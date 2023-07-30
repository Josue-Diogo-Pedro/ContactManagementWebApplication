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
        var version = new MySqlServerVersion(new Version(10, 6, 14));
        var connectionString = configuration.GetConnectionString("MariaDB");
        builder.UseMySql(connectionString, version);

        return new ManagementAppContext(builder.Options);
    }
}
