using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactManagement.App.ViewModels;

namespace ContactManagement.App.Areas.Identity.Data;

public class ContactManagementAppContext : IdentityDbContext<IdentityUser>
{
    public ContactManagementAppContext(DbContextOptions<ContactManagementAppContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var version = new MySqlServerVersion(new Version(10, 6, 14));
        var connectString = "Server=10.25.3.19;DataBase=db;Uid=root;Pwd=root;";


        optionsBuilder.UseMySql(connectString, version);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<ContactManagement.App.ViewModels.ContactViewModel> ContactViewModel { get; set; } = default!;
}
