using Microsoft.EntityFrameworkCore;
using MotorcycleService.Infrastructure.DataContext;
using MotorcycleService;
using Serilog;
using MotorcycleService.Application.Interfaces;
using Refit;

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
builder.Services.AddRefitClient<IRentalService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"));

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
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
