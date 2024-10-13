using DeliveryPilots.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using MotorcycleService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
       .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postegres")));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddSerilog();
    config.AddDebug();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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
