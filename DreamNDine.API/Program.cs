using DreamNDine.BLL.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server.Features;
using DreamNDine.API.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IHousingService, HousingService>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<DreamNDineContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DnDCnStr")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(PropertyProfile));
//builder.Services.AddCors(options =>
//{
//	options.AddPolicy(name: "AllowSpecificOrigin", // You can give any name 
//		policy =>
//		{
//			policy.WithOrigins("https://localhost:7130"); // Replace <client_port> with your client's port number
//		});
//});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

