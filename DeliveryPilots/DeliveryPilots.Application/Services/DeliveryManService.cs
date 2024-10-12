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
            TipoCnh = command.TipoCnh
        };

        bool resultSaveData = await _deliveryManRepository.AddDeliveryManAsync(deliveryMan);

        if (resultSaveData)
        {
            bool result = await SaveImageToLocalDiskAsync(command.ImagemCnh, command.Identificador);
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

    public async Task<bool> SaveImageToLocalDiskAsync(byte[] image, string identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(SaveImageToLocalDiskAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        try
        {
            var directoryPath = Path.Combine("CnhImages", identificador);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, "cnh.jpg");
            await File.WriteAllBytesAsync(filePath, image);

            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogMessages.Finished(nameForLog));
            return false;
        }
    }
}