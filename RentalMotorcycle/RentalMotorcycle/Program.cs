using RentalMotorcycle;
using Microsoft.EntityFrameworkCore;
using RentalMotorcycle.Infrastructure.DataContext;
using Serilog;
using RentalMotorcycle.Application.Interfaces;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddRefitClient<IDeliveryManService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5003"));


Log.Logger = new LoggerConfiguration()
       .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postegres")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
