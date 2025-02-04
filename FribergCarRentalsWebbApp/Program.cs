using FribergCarRentalsWebbApp.Data;
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
            builder.Services.AddScoped<ICustomer, CustomerRepository>();
            builder.Services.AddScoped<IAdministrator, AdministratorRepository>();
            builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();

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

            // Custom middleware to grab and parse custom authorization cookie.
            app.Use(async (context, next) =>
            {
                var authCookie = context.Request.Cookies["UserAuth"];
                if (!string.IsNullOrEmpty(authCookie))
                {
                    // Expected format: "UserId|IsAdmin"
                    var parts = authCookie.Split('|');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int userId) &&
                        bool.TryParse(parts[1], out bool isAdmin))
                    {
                        // Store values in context.Items for later use.
                        context.Items["UserId"] = userId;
                        context.Items["IsAdmin"] = isAdmin;
                    }
                }
                await next.Invoke();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
