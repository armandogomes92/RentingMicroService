using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Application.Handlers.DeliveryMan.Queries;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Application.Services;
using DeliveryPilots.Infrastructure.Interfaces;
using DeliveryPilots.Infrastructure.Repositories;
using System.Reflection;

namespace DeliveryPilots;

public static class DIConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositórios
        services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

        // Serviços
        services.AddScoped<IDeliveryManService, DeliveryManService>();

        //Handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(CreateDeliveryManHandler)),
            Assembly.GetAssembly(typeof(UpdateDeliveryManHandler)),
            Assembly.GetAssembly(typeof(GetCategoryOfDeliveryManHandler))
            ));

        // Logger
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        return services;
    }
}