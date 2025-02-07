using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Middleware;
using FribergCarRentalsWebbApp.Services;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentalsWebbApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(new ConfigurationBuilder()
                                                                                                    .AddJsonFile("appsettings.json")
                                                                                                    .Build()
                                                                                                    .GetSection("ConnectionStrings")["FribergCarRentals"]));
            builder.Services.AddScoped<IAccount, AccountRepository>();
            builder.Services.AddScoped<IBooking, BookingRepository>();
            builder.Services.AddScoped<ICar,  CarRepository>();
            builder.Services.AddScoped<IPrice, PriceRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

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

            // Custom middleware to grab and store custom authorization cookie.
            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
