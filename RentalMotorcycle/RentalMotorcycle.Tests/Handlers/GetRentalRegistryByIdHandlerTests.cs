using Moq;
using Microsoft.Extensions.Logging;
using RentalMotorcycle.Application.Handlers.Rental.Queries;
using RentalMotorcycle.Application.Interfaces;
using RentalMotorcycle.Domain.Models;

namespace RentalMotorcycle.Tests.Handlers;

public class GetRentalRegistryByIdHandlerTests
{
    private readonly Mock<ILogger<GetRentalRegistryByIdHandler>> _loggerMock;
    private readonly Mock<IRentalService> _rentalServiceMock;
    private readonly GetRentalRegistryByIdHandler _handler;

    public GetRentalRegistryByIdHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetRentalRegistryByIdHandler>>();
        _rentalServiceMock = new Mock<IRentalService>();
        _handler = new GetRentalRegistryByIdHandler(_rentalServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_RentalRegistryFound_ReturnsRegistry()
    {
        // Arrange
        var query = new GetRentalRegistryByIdQuery { Identificador = 1 };
        var expectedRegistry = new Rental { Identificador = 1, EntregadorId = "123", MotoId = "456" };
        _rentalServiceMock.Setup(s => s.GetRentalById(It.IsAny<int>())).ReturnsAsync(expectedRegistry);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Equal(expectedRegistry, response.Content);
    }

    [Fact]
    public async Task Handle_RentalRegistryNotFound_ReturnsNull()
    {
        // Arrange
        var query = new GetRentalRegistryByIdQuery { Identificador = 1 };
        _rentalServiceMock.Setup(s => s.GetRentalById(It.IsAny<int>())).ReturnsAsync((Rental)null);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(response.Content);
    }
}