using Moq;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.Rental.Queries;
using RentalMotorcycle.Application.Interfaces;

namespace RentalMotorcycle.Tests.Handlers;

public class CheckMotorcycleIsRentingHandlerTests
{
    private readonly Mock<ILogger<GetRentalRegistryByIdHandler>> _loggerMock;
    private readonly Mock<IRentalService> _rentalServiceMock;
    private readonly CheckMotorcycleIsRentingHandler _handler;

    public CheckMotorcycleIsRentingHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetRentalRegistryByIdHandler>>();
        _rentalServiceMock = new Mock<IRentalService>();
        _handler = new CheckMotorcycleIsRentingHandler(_rentalServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_MotorcycleIsRenting_ReturnsTrue()
    {
        // Arrange
        var query = new CheckMotorcycleIsRentingQuery { Identificador = "1" };
        _rentalServiceMock.Setup(s => s.CheckMotorcycleIsRenting(It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.True((bool)response.Content);
    }

    [Fact]
    public async Task Handle_MotorcycleIsNotRenting_ReturnsFalse()
    {
        // Arrange
        var query = new CheckMotorcycleIsRentingQuery { Identificador = "1" };
        _rentalServiceMock.Setup(s => s.CheckMotorcycleIsRenting(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.False((bool)response.Content);
    }
}