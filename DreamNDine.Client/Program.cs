using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Services;
using Microsoft.EntityFrameworkCore;

namespace DreamNDine.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IHousingService, HousingService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<DreamNDineContext>(options =>
                options.UseSqlServer("Server=SILVERBACK\\SQLEXPRESS;Database=DreamNDine;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
