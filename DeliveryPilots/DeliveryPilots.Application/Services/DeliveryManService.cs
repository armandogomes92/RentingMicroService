using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Models;
using DeliveryPilots.Infrastructure.Interfaces;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Application.Services;

public class DeliveryManService : IDeliveryManService
{
    private readonly ILogger<DeliveryManService> _logger;
    private readonly IDeliveryManRepository _deliveryManRepository;

    private const string NameOfClass = nameof(DeliveryManService);

    public DeliveryManService(ILogger<DeliveryManService> logger, IDeliveryManRepository deliveryManRepository)
    {
        _logger = logger;
        _deliveryManRepository = deliveryManRepository;
    }

    public async Task<bool> CreateDeliveryMan(CreateDeliveryManCommand command)
    {
        string nameForLog = $"{NameOfClass} {nameof(CreateDeliveryMan)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = new DeliveryMan
        {
            Identificador = command.Identificador,
            Nome = command.Nome,
            Cnpj = command.Cnpj,
            DataNascimento = command.DataNascimento,
            NumeroCnh = command.NumeroCnh,
            TipoCnh = command.TipoCnh.ToUpper()
        };

        bool resultSaveData = await _deliveryManRepository.AddDeliveryManAsync(deliveryMan);

        if (resultSaveData)
        {
            bool result = await _deliveryManRepository.InsertOrUpdateImageCnhAsync(command.ImagemCnh, command.Identificador);
            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return result;
        }

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return resultSaveData;
    }

    public async Task<bool> UpdateCnhAsync(UpdateDeliveryManCommand command)
    {
        string nameForLog = $"{NameOfClass} {nameof(UpdateCnhAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        bool result = await _deliveryManRepository.InsertOrUpdateImageCnhAsync(command.ImagemCnh, command.Identificador);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    public async Task<string> GetCnhCategoryAsync(string id)
    {
        string nameForLog = $"{NameOfClass} {nameof(GetCnhCategoryAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        string result = await _deliveryManRepository.GetCnhCategory(id);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    public async Task<bool> CheckIfExistDeliverymanById(string id)
    {
        string nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanById)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        bool result = await _deliveryManRepository.CheckIfExistDeliverymanById(id);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    public async Task<bool> CheckIfExistDeliverymanByCnpj(string cnpj)
    {
        string nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanByCnpj)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        bool result = await _deliveryManRepository.CheckIfExistDeliverymanByCnpj(cnpj);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }

    public async Task<bool> CheckIfExistDeliverymanByCnhNumber(string cnhNumber)
    {
        string nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanByCnhNumber)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        bool result = await _deliveryManRepository.CheckIfExistDeliverymanByCnhNumber(cnhNumber);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return result;
    }
}