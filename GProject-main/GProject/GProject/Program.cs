using Gp.Services;
using GProject.Models;
using GProject.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddScoped<IVnPayService, VnPayService>();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        builder.Services.AddDbContext<NorthwindContext>(
        options => options
            .UseSqlServer(config.GetConnectionString("MyConnectionStrings")));
        var app = builder.Build();

        app.MapControllerRoute(
        name: "default",
        pattern: "/{controller}/{action}/"
        );

        app.MapControllerRoute(
        name: "default",
        pattern: "/{controller}/{action}/{id}"
        );
        app.MapControllerRoute(
       name: "default",
       pattern: "/{controller=Home}/{action=getAllProducts}"
       );
        app.UseSession();
        app.Run();
    }
}