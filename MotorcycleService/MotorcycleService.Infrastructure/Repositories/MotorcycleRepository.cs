﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotorcycleService.Domain.Models;
using MotorcycleService.Infrastructure.DataContext;
using MotorcycleService.Infrastructure.Interfaces;
using MotorcycleService.Infrastructure.Logging;

namespace MotorcycleService.Infrastructure.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MotorcycleRepository> _logger;

    private const string NameOfClass = nameof(MotorcycleRepository);

    public MotorcycleRepository(ApplicationDbContext context, ILogger<MotorcycleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddMotorcycleAsync(Motorcycle moto)
    {
        _logger.LogInformation(LogMessages.Finished($"{NameOfClass} {nameof(MotorcycleRepository.AddMotorcycleAsync)}"));

        await _context.Motorcycle.AddAsync(moto);

        _logger.LogInformation(LogMessages.Finished($"{NameOfClass} {nameof(MotorcycleRepository.AddMotorcycleAsync)}"));

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Motorcycle>> GetAllMotorcyclesAsync()
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass} {nameof(GetAllMotorcyclesAsync)}"));

        var result = await _context.Motorcycle.ToListAsync();

        _logger.LogInformation(LogMessages.Finished($"{NameOfClass} {nameof(GetAllMotorcyclesAsync)}"));

        return result;
    }
    
    public async Task<Motorcycle?> GetMotorcycleByPlateAsync(string plate)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass} {nameof(GetMotorcycleByPlateAsync)}"));

        var normalizedPlate = plate.Trim().ToLower();
        var result = await _context.Motorcycle
            .FirstOrDefaultAsync(s => s.Placa.ToLower().Contains(normalizedPlate));

        _logger.LogInformation(LogMessages.Finished($"{NameOfClass} {nameof(GetMotorcycleByPlateAsync)}"));

        return result;
    }

    public async Task<bool> UpdateMotorcycleByIdAsync(Motorcycle moto)
    {
        _logger.LogInformation(LogMessages.Start($"{NameOfClass} {nameof(UpdateMotorcycleByIdAsync)}"));

        _context.Motorcycle.Update(moto);
        bool saveChanges = await _context.SaveChangesAsync() > 0;

        _logger.LogInformation(LogMessages.Finished($"{NameOfClass} {nameof(UpdateMotorcycleByIdAsync)}"));

        return saveChanges;
    }

    public async Task<Motorcycle> GetMotorcycleByIdAsync(string id)
    {
        return await _context.Motorcycle.FindAsync(id);
    }

    public async Task<bool> DeleteMotorcycleByIdAsync(Motorcycle moto)
    {
        _context.Motorcycle.Remove(moto);
        return await _context.SaveChangesAsync() > 0;
    }
}