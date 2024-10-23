using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Application.Services;
using DeliveryPilots.Domain.Models;
using DeliveryPilots.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DeliveryPilots.Tests.Services;

public class DeliveryManServiceTests
{
    private readonly Mock<IDeliveryManRepository> _deliveryManRepositoryMock;
    private readonly Mock<ILogger<DeliveryManService>> _loggerMock;
    private readonly DeliveryManService _service;

    public DeliveryManServiceTests()
    {
        _deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
        _loggerMock = new Mock<ILogger<DeliveryManService>>();
        _service = new DeliveryManService(_loggerMock.Object, _deliveryManRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateDeliveryMan_Success()
    {
        // Arrange
        var command = new CreateDeliveryManCommand { Identificador = "123", Nome = "John Doe", Cnpj = "456", NumeroCnh = "789", TipoCnh = "B", ImagemCnh = new byte[] { 0x20 } };
        _deliveryManRepositoryMock.Setup(r => r.AddDeliveryManAsync(It.IsAny<DeliveryMan>())).ReturnsAsync(true);
        _deliveryManRepositoryMock.Setup(r => r.InsertOrUpdateImageCnhAsync(command.ImagemCnh, command.Identificador)).ReturnsAsync(true);

        // Act
        var result = await _service.CreateDeliveryMan(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateDeliveryMan_Failure()
    {
        // Arrange
        var command = new CreateDeliveryManCommand { Identificador = "123", Nome = "John Doe", Cnpj = "456", NumeroCnh = "789", TipoCnh = "B", ImagemCnh = new byte[] { 0x20 } };
        _deliveryManRepositoryMock.Setup(r => r.AddDeliveryManAsync(It.IsAny<DeliveryMan>())).ReturnsAsync(false);

        // Act
        var result = await _service.CreateDeliveryMan(command);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateCnhAsync_Success()
    {
        // Arrange
        var command = new UpdateDeliveryManCommand { Identificador = "123", ImagemCnh = new byte[] { 0x20 } };
        _deliveryManRepositoryMock.Setup(r => r.InsertOrUpdateImageCnhAsync(command.ImagemCnh, command.Identificador)).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateCnhAsync(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetCnhCategoryAsync_ReturnsCategory()
    {
        // Arrange
        var id = "123";
        var expectedCategory = "B";
        _deliveryManRepositoryMock.Setup(r => r.GetCnhCategory(id)).ReturnsAsync(expectedCategory);

        // Act
        var result = await _service.GetCnhCategoryAsync(id);

        // Assert
        Assert.Equal(expectedCategory, result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanById_ReturnsTrue()
    {
        // Arrange
        var id = "123";
        _deliveryManRepositoryMock.Setup(r => r.CheckIfExistDeliverymanById(id)).ReturnsAsync(true);

        // Act
        var result = await _service.CheckIfExistDeliverymanById(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanByCnpj_ReturnsTrue()
    {
        // Arrange
        var cnpj = "456";
        _deliveryManRepositoryMock.Setup(r => r.CheckIfExistDeliverymanByCnpj(cnpj)).ReturnsAsync(true);

        // Act
        var result = await _service.CheckIfExistDeliverymanByCnpj(cnpj);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanByCnhNumber_ReturnsTrue()
    {
        // Arrange
        var cnhNumber = "789";
        _deliveryManRepositoryMock.Setup(r => r.CheckIfExistDeliverymanByCnhNumber(cnhNumber)).ReturnsAsync(true);

        // Act
        var result = await _service.CheckIfExistDeliverymanByCnhNumber(cnhNumber);

        // Assert
        Assert.True(result);
    }
}