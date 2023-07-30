using ContactManagement.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Data.Context;

public class ManagementAppContext : DbContext
{
	public ManagementAppContext(DbContextOptions<ManagementAppContext> options) : base(options) { }

	public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("");
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var proprerty in builder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(fornecdor => fornecdor.ClrType == typeof(string))))
            proprerty.SetColumnType("varchar(100)");

        builder.ApplyConfigurationsFromAssembly(typeof(ManagementAppContext).Assembly);

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(builder);
    }
}
