using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Domain.Models;
using RentalMotorcycle.Infrastructure.DataContext;
using RentalMotorcycle.Infrastructure.Interfaces;
using RentalMotorcycle.Infrastructure.Logging;
using RentalMotorcycle.Infrastructure.Migrations;

namespace RentalMotorcycle.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RentalRepository> _logger;

    private const string NameOfClass = nameof(RentalRepository);

    public RentalRepository(ApplicationDbContext context, ILogger<RentalRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddRentalRegistryAsync(Domain.Models.Rental rental)
    {
        var nameForLog = $"{NameOfClass} {nameof(AddRentalRegistryAsync)}";

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        if (CheckMotorcycleAvailability(rental.MotoId).Result)
        {
            _logger.LogError(LogMessages.Finished(nameForLog));
            return false;
        }
        rental.Rented = true;
        await _context.Rental.AddAsync(rental);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Domain.Models.Rental> GetRentalByIdAsync(int identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(GetRentalByIdAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var rental = await _context.Rental.FindAsync(identificador);

        if (rental == null)
        {
            _logger.LogError(LogMessages.Finished(nameForLog));
            return null;
        }

        rental.ValorDiaria = GetDailyRentalPrice(rental.Plano);

        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return rental;
    }

    public async Task<bool> EndRentalAsync(Domain.Models.Rental rentalToFinish)
    {
        var nameForLog = $"{NameOfClass} {nameof(EndRentalAsync)}";
        _logger.LogInformation(LogMessages.Start(nameForLog));

        rentalToFinish.DataInicio = DateTime.SpecifyKind(rentalToFinish.DataInicio, DateTimeKind.Utc);
        rentalToFinish.DataTermino = DateTime.SpecifyKind(rentalToFinish.DataTermino, DateTimeKind.Utc);
        rentalToFinish.DataPrevisaoTermino = DateTime.SpecifyKind(rentalToFinish.DataPrevisaoTermino, DateTimeKind.Utc);

        _context.Rental.Update(rentalToFinish);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CheckMotorcycleAvailability(string motorcycleId)
    {
        var nameForLog = $"{NameOfClass} {nameof(CheckMotorcycleAvailability)}";
        var check = await _context.Rental.Where(s => s.MotoId == motorcycleId && s.Rented).FirstOrDefaultAsync();
        return check != null;
    }

    private float GetDailyRentalPrice(int plano)
    {
        switch (plano)
        {
            case 7: return 30;
            case 15: return 28;
            case 30: return 22;
            case 45: return 20;
            case 50: return 18;
            default: throw new Exception("Plano de locação inválido.");
        }
    }

    public decimal CalculateTotalRentingCost(Domain.Models.Rental rental)
    {
        var nameForLog = $"{NameOfClass} {nameof(CalculateTotalRentingCost)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var valorDiaria = (decimal)GetDailyRentalPrice(rental.Plano);
        var diasLocacao = (rental.DataTermino - rental.DataInicio).Days;
        var diasUtilizados = (rental.DataDevolucao!.Value - rental.DataInicio).Days;

        decimal valorTotal = diasUtilizados * valorDiaria;

        if (rental.DataDevolucao! < rental.DataPrevisaoTermino)
        {
            var diasNaoEfetivados = (rental.DataPrevisaoTermino - rental.DataDevolucao!.Value).Days;
            decimal multa = 0;

            switch (rental.Plano)
            {
                case 7:
                    multa = diasNaoEfetivados * valorDiaria * 0.20m;
                    break;
                case 15:
                    multa = diasNaoEfetivados * valorDiaria * 0.40m;
                    break;
            }

            valorTotal += multa;
        }
        else if (rental.DataDevolucao! > rental.DataPrevisaoTermino)
        {
            var diasAdicionais = (rental.DataDevolucao!.Value - rental.DataPrevisaoTermino).Days;
            valorTotal += diasAdicionais * 50;
        }

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return valorTotal;
    }
}