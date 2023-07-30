using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ContactManagement.App.Areas.Identity.Data;
using ContactManagement.Data.Context;

namespace ContactManagement.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MariaDB") ?? throw new InvalidOperationException("Connection string 'ContactManagementAppContextConnection' not found.");

            var version = new MySqlServerVersion(new Version(10, 6, 14));

            builder.Services.AddCors();

            builder.Services.AddDbContext<ContactManagementAppContext>(options => options.UseMySql(connectionString, version));
            builder.Services.AddDbContext<ManagementAppContext>(options => options.UseMySql(connectionString, version));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ContactManagementAppContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}