using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RentalMotorcycle.Application.Services;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Models;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Infrastructure.Interfaces;

namespace RentalMotorcycle.Tests.Services;

public class RentalServiceTests
{
    private readonly Mock<ILogger<RentalService>> _loggerMock;
    private readonly Mock<IRentalRepository> _rentalRepositoryMock;
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<IDeliveryManService> _deliveryManServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly HttpClient _httpClient;
    private readonly RentalService _service;

    public RentalServiceTests()
    {
        _loggerMock = new Mock<ILogger<RentalService>>();
        _rentalRepositoryMock = new Mock<IRentalRepository>();
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _deliveryManServiceMock = new Mock<IDeliveryManService>();
        _configurationMock = new Mock<IConfiguration>();
        _httpClient = new HttpClient();
        _configurationMock.Setup(c => c["ExternalApi:BaseUrl"]).Returns("http://example.com");
        _service = new RentalService(_loggerMock.Object, _rentalRepositoryMock.Object, _httpClient, _configurationMock.Object, _rabbitMqServiceMock.Object, _deliveryManServiceMock.Object);
    }

    [Fact]
    public async Task CreateRentalRegistry_Success_ReturnsTrue()
    {
        // Arrange
        var command = new CreateRentalRegistryCommand
        {
            EntregadorId = "123",
            MotoId = "456",
            DataInicio = DateTime.Now,
            DataTermino = DateTime.Now.AddDays(1),
            DataPrevisaoTermino = DateTime.Now.AddDays(1),
            Plano = 7
        };
        _rentalRepositoryMock.Setup(r => r.AddRentalRegistryAsync(It.IsAny<Rental>())).ReturnsAsync(true);

        // Act
        var result = await _service.CreateRentalRegistry(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRentalById_RentalExists_ReturnsRental()
    {
        // Arrange
        var rental = new Rental { Identificador = 1, EntregadorId = "123", MotoId = "456" };
        _rentalRepositoryMock.Setup(r => r.GetRentalByIdAsync(It.IsAny<int>())).ReturnsAsync(rental);

        // Act
        var result = await _service.GetRentalById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rental, result);
    }

    [Fact]
    public async Task CheckMotorcycleIsRenting_MotorcycleIsRenting_ReturnsTrue()
    {
        // Arrange
        _rentalRepositoryMock.Setup(r => r.CheckMotorcycleAvailability(It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _service.CheckMotorcycleIsRenting("1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task EndRentalAsync_Success_ReturnsTrue()
    {
        // Arrange
        var command = new UpdateRentalRegistryCommand
        {
            Identificador = 1,
            DataDevolucao = DateTime.Now
        };
        var rental = new Rental { Identificador = 1, EntregadorId = "123", MotoId = "456", Rented = true };
        _rentalRepositoryMock.Setup(r => r.GetRentalByIdAsync(It.IsAny<int>())).ReturnsAsync(rental);
        _rentalRepositoryMock.Setup(r => r.CalculateTotalRentingCost(It.IsAny<Rental>())).Returns(100);
        _rentalRepositoryMock.Setup(r => r.EndRentalAsync(It.IsAny<Rental>())).ReturnsAsync(true);

        // Act
        var result = await _service.EndRentalAsync(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckDeliveryManCnh_ValidCnh_ReturnsTrue()
    {
        // Arrange
        _deliveryManServiceMock.Setup(d => d.GetCnhType(It.IsAny<string>())).ReturnsAsync("A");

        // Act
        var result = await _service.CheckDeliveryManCnh("123");

        // Assert
        Assert.True(result);
    }
}