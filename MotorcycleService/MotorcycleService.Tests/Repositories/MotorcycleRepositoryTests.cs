using MotorcycleService.Domain.Models;
using MotorcycleService.Infrastructure.DataContext;
using MotorcycleService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace MotorcycleService.Tests.Repositories;

public class MotorcycleRepositoryTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly Mock<ILogger<MotorcycleRepository>> _mockLogger;
    private readonly MotorcycleRepository _repository;

    public MotorcycleRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _mockLogger = new Mock<ILogger<MotorcycleRepository>>();
        _mockContext = new Mock<ApplicationDbContext>(options);
        _repository = new MotorcycleRepository(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task AddMotorcycleAsync_ShouldAddMotorcycle()
    {
        var motorcycle = new Motorcycle { Identificador = "1", Placa = "ABC1234", Ano = 2022, Modelo = "TestModel" };
        _mockContext.Setup(c => c.Motorcycle.AddAsync(motorcycle, It.IsAny<CancellationToken>())).Returns(new ValueTask<EntityEntry<Motorcycle>>(Task.FromResult((EntityEntry<Motorcycle>)null!)));
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _repository.AddMotorcycleAsync(motorcycle);

        Assert.True(result);
    }

    [Fact]
    public async Task GetAllMotorcyclesAsync_ShouldReturnAllMotorcycles()
    {

        var motorcycles = new List<Motorcycle>
    {
        new Motorcycle { Identificador = "1", Placa = "ABC1234", Ano = 2023, Modelo = "TestModel" },
        new Motorcycle { Identificador = "2", Placa = "ABC5678", Ano = 2022, Modelo = "TestModel2" }
    };

        _mockContext.Setup(c => c.Motorcycle).ReturnsDbSet(motorcycles);


        var result = await _repository.GetAllMotorcyclesAsync();


        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateMotorcycleByIdAsync_ShouldUpdateMotorcycle()
    {

        var motorcycle = new Motorcycle { Identificador = "1", Placa = "ABC1234" };
        _mockContext.Setup(c => c.Motorcycle.Update(motorcycle));
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


        var result = await _repository.UpdateMotorcycleByIdAsync(motorcycle);


        Assert.True(result);
    }

    [Fact]
    public async Task GetMotorcycleByIdAsync_ShouldReturnMotorcycle_WhenMotorcycleExists()
    {

        var motorcycle = new Motorcycle { Identificador = "1", Placa = "ABC1234" };
        _mockContext.Setup(c => c.Motorcycle.FindAsync("1")).ReturnsAsync(motorcycle);


        var result = await _repository.GetMotorcycleByIdAsync("1");


        Assert.NotNull(result);
        Assert.Equal("1", result.Identificador);
    }

    [Fact]
    public async Task DeleteMotorcycleByIdAsync_ShouldDeleteMotorcycle()
    {

        var motorcycle = new Motorcycle { Identificador = "1", Placa = "ABC1234" };
        _mockContext.Setup(c => c.Motorcycle.Remove(motorcycle));
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


        var result = await _repository.DeleteMotorcycleByIdAsync(motorcycle);


        Assert.True(result);
    }
}