using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Handlers.Rental.Queries;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Application.Services;
using RentalMotorcycle.Infrastructure.Interfaces;
using RentalMotorcycle.Infrastructure.Messaging;
using RentalMotorcycle.Infrastructure.Repositories;
using System.Reflection;

namespace RentalMotorcycle;

public static class DIConfigurations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do RabbitMQ
        services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value);
        services.AddSingleton<RabbitMQClient>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        // Serviços
        services.AddScoped<IRentalService, RentalService>();

        // Repositórios
        services.AddScoped<IRentalRepository, RentalRepository>();

        //Handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(CreateRentalRegistryHandler)),
            Assembly.GetAssembly(typeof(UpdateRentalRegistryHandler)),
            Assembly.GetAssembly(typeof(GetRentalRegistryByIdHandler)),
            Assembly.GetAssembly(typeof(CheckMotorcycleIsRentingHandler))
            ));

        // Logger
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        return services;
    }
}