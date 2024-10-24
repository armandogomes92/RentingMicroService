﻿using RentalMotorcycle.Domain.Models;

namespace RentalMotorcycle.Infrastructure.Interfaces;

public interface IRentalRepository
{
    Task<bool> AddRentalRegistryAsync(Rental rental);
    Task<Rental> GetRentalByIdAsync(int identificador);
    Task<bool> EndRentalAsync(Domain.Models.Rental rental);
    Task<bool> CheckMotorcycleAvailability(string motorcycleId);
    decimal CalculateTotalRentingCost(Rental rental);
}