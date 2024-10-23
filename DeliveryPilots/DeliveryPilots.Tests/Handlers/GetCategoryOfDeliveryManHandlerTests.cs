using DeliveryPilots.Application.Handlers.DeliveryMan.Queries;
using DeliveryPilots.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace DeliveryPilots.Tests.Handlers;

public class GetCategoryOfDeliveryManHandlerTests
{
    private readonly Mock<IDeliveryManService> _deliveryManServiceMock;
    private readonly Mock<ILogger<GetCategoryOfDeliveryManHandler>> _loggerMock;
    private readonly GetCategoryOfDeliveryManHandler _handler;

    public GetCategoryOfDeliveryManHandlerTests()
    {
        _deliveryManServiceMock = new Mock<IDeliveryManService>();
        _loggerMock = new Mock<ILogger<GetCategoryOfDeliveryManHandler>>();
        _handler = new GetCategoryOfDeliveryManHandler(_loggerMock.Object, _deliveryManServiceMock.Object);
    }

    [Fact]
    public async Task Handle_CategoryNotFound_ReturnsFalse()
    {
        // Arrange
        var query = new GetCategoryOfDeliveryManQuery { Identificador = "123" };
        _deliveryManServiceMock.Setup(s => s.GetCnhCategoryAsync(query.Identificador)).ReturnsAsync(string.Empty);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.False((bool)response.Content);
    }

    [Fact]
    public async Task Handle_CategoryFound_ReturnsCategory()
    {
        // Arrange
        var query = new GetCategoryOfDeliveryManQuery { Identificador = "123" };
        var expectedCategory = "B";
        _deliveryManServiceMock.Setup(s => s.GetCnhCategoryAsync(query.Identificador)).ReturnsAsync(expectedCategory);

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.Content);
        Assert.Equal(expectedCategory, response.Content);
    }
}