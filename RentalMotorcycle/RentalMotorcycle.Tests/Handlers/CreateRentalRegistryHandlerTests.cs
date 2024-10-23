using Moq;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Create;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Tests.Handlers;

public class CreateRentalRegistryHandlerTests
{
    private readonly Mock<ILogger<CreateRentalRegistryHandler>> _loggerMock;
    private readonly Mock<IRentalService> _rentalServiceMock;
    private readonly CreateRentalRegistryHandler _handler;

    public CreateRentalRegistryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateRentalRegistryHandler>>();
        _rentalServiceMock = new Mock<IRentalService>();
        _handler = new CreateRentalRegistryHandler(_loggerMock.Object, _rentalServiceMock.Object);
    }

    [Fact]
    public async Task Handle_InvalidCnh_ReturnsInvalidCnhMessage()
    {
        // Arrange
        var command = new CreateRentalRegistryCommand
        {
            EntregadorId = "1",
            MotoId = "1",
            DataInicio = DateTime.Now.AddDays(1),
            DataTermino = DateTime.Now.AddDays(8),
            DataPrevisaoTermino = DateTime.Now.AddDays(8),
            Plano = 7
        };
        _rentalServiceMock.Setup(s => s.CheckDeliveryManCnh(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.InvalidCnh, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_MotorcycleIsRenting_ReturnsMotorcycleIsRentingMessage()
    {
        // Arrange
        var command = new CreateRentalRegistryCommand
        {
            EntregadorId = "1",
            MotoId = "1",
            DataInicio = DateTime.Now.AddDays(1),
            DataTermino = DateTime.Now.AddDays(8),
            DataPrevisaoTermino = DateTime.Now.AddDays(8),
            Plano = 7
        };
        _rentalServiceMock.Setup(s => s.CheckDeliveryManCnh(It.IsAny<string>())).ReturnsAsync(true);
        _rentalServiceMock.Setup(s => s.CheckMotorcycleIsRenting(It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.MotorcycleIsRenting, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_CreateRentalRegistryFails_ReturnsInvalidDataMessage()
    {
        // Arrange
        var command = new CreateRentalRegistryCommand
        {
            EntregadorId = "1",
            MotoId = "1",
            DataInicio = DateTime.Now.AddDays(1),
            DataTermino = DateTime.Now.AddDays(8),
            DataPrevisaoTermino = DateTime.Now.AddDays(8),
            Plano = 7
        };
        _rentalServiceMock.Setup(s => s.CheckDeliveryManCnh(It.IsAny<string>())).ReturnsAsync(true);
        _rentalServiceMock.Setup(s => s.CheckMotorcycleIsRenting(It.IsAny<string>())).ReturnsAsync(false);
        _rentalServiceMock.Setup(s => s.CreateRentalRegistry(It.IsAny<CreateRentalRegistryCommand>())).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.InvalidData, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_Success_ReturnsResult()
    {
        // Arrange
        var command = new CreateRentalRegistryCommand
        {
            EntregadorId = "1",
            MotoId = "1",
            DataInicio = DateTime.Now.AddDays(1),
            DataTermino = DateTime.Now.AddDays(8),
            DataPrevisaoTermino = DateTime.Now.AddDays(8),
            Plano = 7
        };
        var expectedResult = new Response { Content = true };
        _rentalServiceMock.Setup(s => s.CheckDeliveryManCnh(It.IsAny<string>())).ReturnsAsync(true);
        _rentalServiceMock.Setup(s => s.CheckMotorcycleIsRenting(It.IsAny<string>())).ReturnsAsync(false);
        _rentalServiceMock.Setup(s => s.CreateRentalRegistry(It.IsAny<CreateRentalRegistryCommand>())).ReturnsAsync(true);


        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, response);
    }
}