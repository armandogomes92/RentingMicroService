using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using Microsoft.Extensions.Logging;
using Moq;

namespace DeliveryPilots.Tests.Handlers;

public class UpdateDeliveryManHandlerTests
{
    private readonly Mock<IDeliveryManService> _deliveryManServiceMock;
    private readonly UpdateDeliveryManHandler _handler;
    private readonly Mock<ILogger<UpdateDeliveryManHandler>> _loggerMock;

    public UpdateDeliveryManHandlerTests()
    {
        _deliveryManServiceMock = new Mock<IDeliveryManService>();
        _loggerMock = new Mock<ILogger<UpdateDeliveryManHandler>>();
        _handler = new UpdateDeliveryManHandler(_deliveryManServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_DeliveryManNotFound_ReturnsDeliveryManNotFoundMessage()
    {
        // Arrange
        var command = new UpdateDeliveryManCommand { Identificador = "123", ImagemCnh = new byte[] { 0x20 } };
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.DeliveryManNotFound, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_UpdateSuccessful_ReturnsTrue()
    {
        // Arrange
        var command = new UpdateDeliveryManCommand { Identificador = "123", ImagemCnh = new byte[] { 0x20 } };
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(true);
        _deliveryManServiceMock.Setup(s => s.UpdateCnhAsync(command)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True((bool)response.Content);
    }
}