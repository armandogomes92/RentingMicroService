using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Handlers.Rental.Queries;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Models;
using RentalMotorcycle.Infrastructure.Interfaces;
using RentalMotorcycle.Infrastructure.Logging;

namespace RentalMotorcycle.Application.Services;

public class RentalService : IRentalService
{
    private readonly ILogger<RentalService> _logger;
    private readonly IRentalRepository _rentalRepository;
    private readonly IRabbitMqService _rabbitMqService;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly IDeliveryManService _deliveryManService;

    private const string NameOfClass = nameof(RentalService);

    public RentalService(ILogger<RentalService> logger, IRentalRepository deliveryManRepository, HttpClient httpClient, IConfiguration configuration, IRabbitMqService rabbitMqService, IDeliveryManService deliveryManService)
    {
        _logger = logger;
        _rentalRepository = deliveryManRepository;
        _httpClient = httpClient;
        _baseUrl = configuration["ExternalApi:BaseUrl"]!;
        _rabbitMqService = rabbitMqService;
        _deliveryManService = deliveryManService;
    }

    public async Task<bool> CreateRentalRegistry(CreateRentalRegistryCommand command)
    {
        string nameForLog = $"{NameOfClass} {nameof(CreateRentalRegistry)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        if (! await CheckDeliveryManCnh(command.EntregadorId))
        {
            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return false;
        }

        var rental = new Rental
        {
            EntregadorId = command.EntregadorId,
            MotoId = command.MotoId,
            DataInicio = command.DataInicio,
            DataTermino = command.DataTermino,
            DataPrevisaoTermino = command.DataPrevisaoTermino,
            Plano = command.Plano,
            Rented = true
        };

        bool resultSaveData = await _rentalRepository.AddRentalRegistryAsync(rental);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return resultSaveData;
    }

    public async Task<Rental> GetRentalById(int id)
    {
        string nameForLog = $"{NameOfClass} {nameof(GetRentalById)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        Rental result = await _rentalRepository.GetRentalByIdAsync(id);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }
    
    public async Task<bool> CheckMotorcycleIsRenting(string identificador)
    {
        string nameForLog = $"{NameOfClass} {nameof(CreateRentalRegistry)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        bool result = await _rentalRepository.CheckMotorcycleAvailability(identificador);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    public async Task<bool> EndRentalAsync(UpdateRentalRegistryCommand command)
    {
        var nameForLog = $"{NameOfClass} {nameof(EndRentalAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var rental = await GetRentalById(command.Identificador);
        rental.DataDevolucao = command.DataDevolucao;
        rental.ValorTotal = _rentalRepository.CalculateTotalRentingCost(rental);
        rental.Rented = false;

        var messege = new {Id = rental.EntregadorId, ValorTotal = rental.ValorTotal };
        _rabbitMqService.PublishTotalPriceOfRental(messege);

        var result = await _rentalRepository.EndRentalAsync(rental);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    private async Task<bool> CheckDeliveryManCnh(string deliveryManId)
    {
        var nameForLog = $"{NameOfClass} {nameof(CheckDeliveryManCnh)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var cnhType = await _deliveryManService.GetCnhType(deliveryManId);

        return cnhType.ToUpper().Contains("A");
    }
}