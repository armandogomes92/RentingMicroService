using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Infrastructure.Interfaces;
using MotorcycleService.Domain.Models;
using Refit;

namespace MotorcycleService.Application.Services;
public class MotorcycleService : IMotorcycleService
{
    private readonly IMotorcycleRepository _motoRepository;
    private readonly IRabbitMqService _rabbitMqService;
    private readonly ILogger<UpdateMotorcyclePlateHandler> _logger;

    private const string NameOfClass = nameof(MotorcycleService);
    private const int MotorcycleYearForNotification = 2024;

    public MotorcycleService(IMotorcycleRepository motoRepository, IRabbitMqService rabbitMqService, ILogger<UpdateMotorcyclePlateHandler> logger)
    {
        _motoRepository = motoRepository;
        _rabbitMqService = rabbitMqService;
        _logger = logger;
    }

    public async Task<bool> CreateMotorcycleAsync(CreateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        string nameForLog = $"{NameOfClass} {nameof(CreateMotorcycleAsync)}";
        _logger.LogInformation(LogMessages.Start(nameForLog));

        var response = new Motorcycle
        {
            Identificador = command.Identificador,
            Ano = command.Ano,
            Modelo = command.Modelo,
            Placa = command.Placa
        };

        var result = await _motoRepository.AddMotorcycleAsync(response);

        if (result)
        {
            _rabbitMqService.PublishRegisteredMotorcycle(Messages.RegisteredMotorcycle);

            if (response.Ano == MotorcycleYearForNotification)
                _rabbitMqService.SendYear2024Message(Messages.Motorcycle2024);

            _logger.LogInformation(LogMessages.Finished(nameForLog));

            return result;
        }

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return result;
    }
    public async Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync()
    {
        string nameForLog = $"{NameOfClass} {nameof(GetMotorcyclesAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var result = await _motoRepository.GetAllMotorcyclesAsync();

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return result;
    }
    public async Task<bool> UpdateMotorcycleByIdAsync(UpdateMotorcycleCommand command, CancellationToken none)
    {
        string nameForLog = $"{NameOfClass} {nameof(UpdateMotorcycleByIdAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));
        var existingMoto = await _motoRepository.GetMotorcycleByIdAsync(command.Identificador);

        if (existingMoto == null)
        {
            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return false;
        }

        existingMoto.Placa = command.Placa;

        bool result = await _motoRepository.UpdateMotorcycleByIdAsync(existingMoto);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return result;
    }
    public async Task<Motorcycle> GetMotorcycleByIdAsync(GetMotorcycleByIdQuery query)
    {
        string nameForLog = $"{NameOfClass} {nameof(GetMotorcycleByIdAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var result = await _motoRepository.GetMotorcycleByIdAsync(query.Id);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return result;
    }
    public async Task<bool> DeleteMotorcycleByIdAsync(DeleteMotorcycleByIdCommand command, CancellationToken cancellationToken)
    {
        var nameForLog = $"{NameOfClass} {nameof(DeleteMotorcycleByIdAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var existingMoto = await _motoRepository.GetMotorcycleByIdAsync(command.Id);


        if (existingMoto == null || CheckMotorcycleRental(existingMoto.Identificador).Result)
        {
            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return false;
        }

        bool result = await _motoRepository.DeleteMotorcycleByIdAsync(existingMoto);
        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return result;
    }
    private async Task<bool> CheckMotorcycleRental(string identificador)
    {
        var rest = RestService.For<IRentalService>("https://localhost:5001");

        return await rest.CheckMotorcycleIsRenting(identificador);
    }
}