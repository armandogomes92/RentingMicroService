using RentalMotorcycle;
using Microsoft.EntityFrameworkCore;
using RentalMotorcycle.Infrastructure.DataContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using RentalMotorcycle.Application.Interfaces;
using Refit;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Handlers.Rental.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5002);
});

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateRentalRegistryHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateRentalRegistryHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<CheckMotorcycleIsRentingHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<GetRentalRegistryByIdHandler>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddRefitClient<IDeliveryManService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://host.docker.internal:5003"));

Log.Logger = new LoggerConfiguration()
       .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postegres")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedHosts",
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

app.MapControllers();

app.Run();
