using Moq;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.Rental.Commands.Update;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Tests.Handlers;

public class UpdateRentalRegistryHandlerTests
{
    private readonly Mock<ILogger<UpdateRentalRegistryHandler>> _loggerMock;
    private readonly Mock<IRentalService> _rentalServiceMock;
    private readonly UpdateRentalRegistryHandler _handler;

    public UpdateRentalRegistryHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRentalRegistryHandler>>();
        _rentalServiceMock = new Mock<IRentalService>();
        _handler = new UpdateRentalRegistryHandler(_rentalServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_EndRentalFails_ReturnsInvalidDataMessage()
    {
        // Arrange
        var command = new UpdateRentalRegistryCommand
        {
            Identificador = 1,
            DataDevolucao = DateTime.Now
        };
        _rentalServiceMock.Setup(s => s.EndRentalAsync(It.IsAny<UpdateRentalRegistryCommand>())).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.InvalidData, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_Success_ReturnsReturnedDateMessage()
    {
        // Arrange
        var command = new UpdateRentalRegistryCommand
        {
            Identificador = 1,
            DataDevolucao = DateTime.Now
        };
        _rentalServiceMock.Setup(s => s.EndRentalAsync(It.IsAny<UpdateRentalRegistryCommand>())).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Contains(Messages.ReturnedDate, response.Content.ToString());
    }
}