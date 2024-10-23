using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Domain.Models;
using RentalMotorcycle.Infrastructure.DataContext;
using RentalMotorcycle.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RentalMotorcycle.Tests.Repositories;

public class RentalRepositoryTests
{
    private readonly Mock<ApplicationDbContext> _contextMock;
    private readonly Mock<ILogger<RentalRepository>> _loggerMock;
    private readonly RentalRepository _repository;

    public RentalRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _contextMock = new Mock<ApplicationDbContext>(options);
        _loggerMock = new Mock<ILogger<RentalRepository>>();
        _repository = new RentalRepository(_contextMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddRentalRegistryAsync_RentalAdded_ReturnsTrue()
    {
        // Arrange
        var rental = new Rental
        {
            EntregadorId = "1",
            MotoId = "456",
            Rented = false,
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2)
        };

        var rentalDbSetMock = new Mock<DbSet<Rental>>();
        _contextMock.Setup(c => c.Rental).Returns(rentalDbSetMock.Object);
        rentalDbSetMock.Setup(d => d.AddAsync(It.IsAny<Rental>(), It.IsAny<CancellationToken>()))
                       .Returns((Rental r, CancellationToken ct) =>
                       {
                           _contextMock.Object.Rental.Add(r);
                           return new ValueTask<EntityEntry<Rental>>(Task.FromResult((EntityEntry<Rental>)null));
                       });

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _repository.AddRentalRegistryAsync(rental);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRentalByIdAsync_RentalExists_ReturnsRental()
    {
        // Arrange
        var rental = new Rental
        {
            EntregadorId = "1",
            MotoId = "456",
            Rented = true,
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2)
        };
        _contextMock.Setup(c => c.Rental.FindAsync(It.IsAny<int>())).ReturnsAsync(rental);

        // Act
        var result = await _repository.GetRentalByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rental, result);
    }

    [Fact]
    public async Task EndRentalAsync_RentalUpdated_ReturnsTrue()
    {
        // Arrange
        var rental = new Rental
        {
            EntregadorId = "1",
            MotoId = "456",
            Rented = true,
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2)
        };

        _contextMock.Setup(c => c.Rental.Update(It.IsAny<Rental>()));
        _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _repository.EndRentalAsync(rental);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckMotorcycleAvailability_MotorcycleIsAvailable_Returns_True()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new ApplicationDbContext(options);
        var loggerMock = new Mock<ILogger<RentalRepository>>();
        var repository = new RentalRepository(context, loggerMock.Object);

        var motorcycleId = "456";
        var rental = new Rental
        {
            EntregadorId = "1",
            MotoId = motorcycleId,
            Rented = true,
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2)
        };

        context.Rental.Add(rental);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.CheckMotorcycleAvailability(motorcycleId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckMotorcycleAvailability_MotorcycleIsAvailable_Returns_False()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new ApplicationDbContext(options);
        var loggerMock = new Mock<ILogger<RentalRepository>>();
        var repository = new RentalRepository(context, loggerMock.Object);

        var motorcycleId = "456";
        var rental = new Rental
        {
            EntregadorId = "1",
            MotoId = motorcycleId,
            Rented = false,
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2)
        };

        context.Rental.Add(rental);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.CheckMotorcycleAvailability(motorcycleId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalculateTotalRentingCost_ValidRental_ReturnsCorrectCost()
    {
        // Arrange
        var rental = new Rental
        {
            Plano = 7,
            DataInicio = DateTime.Now.AddDays(-10),
            DataTermino = DateTime.Now.AddDays(-3),
            DataPrevisaoTermino = DateTime.Now.AddDays(-2),
            DataDevolucao = DateTime.Now
        };

        // Act
        var result = _repository.CalculateTotalRentingCost(rental);

        // Assert
        Assert.True(result > 0);
    }
}