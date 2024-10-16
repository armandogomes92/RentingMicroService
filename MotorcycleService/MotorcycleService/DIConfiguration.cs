using Microsoft.Extensions.Options;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Application.Services;
using MotorcycleService.Infrastructure.Interfaces;
using MotorcycleService.Infrastructure.Messaging;
using MotorcycleService.Infrastructure.Repositories;
using System.Reflection;

namespace MotorcycleService;

public static class DIConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do RabbitMQ
        services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMQ"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value);

        services.AddSingleton<RabbitMQClient>();

        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        // Repositórios
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

        // Serviços
        services.AddScoped<IMotorcycleService, Application.Services.MotorcycleService>();

        //Handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(CreateMotorcycleHandler)),
            Assembly.GetAssembly(typeof(DeleteMotorcycleByIdHandler)),
            Assembly.GetAssembly(typeof(UpdateMotorcyclePlateHandler)),
            Assembly.GetAssembly(typeof(GetMotorcyclesHandler)),
            Assembly.GetAssembly(typeof(GetMotorcycleByIdHandler))
            ));

        // Logger
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        return services;
    }
}