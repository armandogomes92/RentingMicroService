﻿using DeliveryPilots.Domain.Models;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.DataContext;
using DeliveryPilots.Infrastructure.Interfaces;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Infrastructure.Repositories;

public class DeliveryManRepository : IDeliveryManRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DeliveryManRepository> _logger;

    private const string NameOfClass = nameof(DeliveryManRepository);

    public DeliveryManRepository(ApplicationDbContext context, ILogger<DeliveryManRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddDeliveryManAsync(DeliveryMan deliveryMan)
    {
        var nameForLog = $"{NameOfClass} {nameof(AddDeliveryManAsync)}";

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        await _context.DeliveryMan.AddAsync(deliveryMan);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> InsertOrUpdateImageCnhAsync(byte[] cngImage, string identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(InsertOrUpdateImageCnhAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var directoryPath = Path.Combine("LocalImages", identificador);
        var filePath = Path.Combine(directoryPath, "cnh.jpg");

        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllBytesAsync(filePath, cngImage);

            _logger.LogInformation(LogMessages.Finished(nameForLog));
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogMessages.Finished(nameForLog));
            return false;
        }
    }

    public async Task<string> GetCnhCategory(string identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(GetCnhCategory)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = await _context.DeliveryMan.FindAsync(identificador);

        if (deliveryMan == null)
        {
            _logger.LogError(LogMessages.Finished(nameForLog));
            return null;
        }
        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return deliveryMan.TipoCnh;
    }

    public async Task<bool> CheckIfExistDeliverymanById(string id)
    {
        var nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanById)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = await _context.DeliveryMan.FindAsync(id);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return deliveryMan != null;
    }

    public async Task<bool> CheckIfExistDeliverymanByCnpj(string cnpj)
    {
        var nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanByCnpj)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = await _context.DeliveryMan.FirstOrDefaultAsync(x => x.Cnpj == cnpj);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return deliveryMan != null;
    }

    public async Task<bool> CheckIfExistDeliverymanByCnhNumber(string cnhNumber)
    {
        var nameForLog = $"{NameOfClass} {nameof(CheckIfExistDeliverymanByCnhNumber)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = await _context.DeliveryMan.FirstOrDefaultAsync(x => x.NumeroCnh == cnhNumber);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return deliveryMan != null;
    }
}