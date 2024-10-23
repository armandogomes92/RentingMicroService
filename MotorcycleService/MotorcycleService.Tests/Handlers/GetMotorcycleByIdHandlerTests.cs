using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Resources;

namespace MotorcycleService.Tests.Handlers;

public class GetMotorcycleByIdHandlerTests
{
    private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
    private readonly GetMotorcycleByIdHandler _handler;

    public GetMotorcycleByIdHandlerTests()
    {
        _motorcycleServiceMock = new Mock<IMotorcycleService>();
        _handler = new GetMotorcycleByIdHandler(_motorcycleServiceMock.Object, Mock.Of<ILogger<GetMotorcycleByIdHandler>>());
    }

    [Fact]
    public async Task Handle_ShouldReturnMotorcycle_WhenMotorcycleExists()
    {
        var query = new GetMotorcycleByIdQuery { Id = "123" };
        var motorcycle = new MotorcycleService.Domain.Models.Motorcycle { Identificador = "123", Placa = "ABC-1234" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcycleByIdAsync(query.Id))
                              .ReturnsAsync(motorcycle);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(motorcycle, result.Content);
        _motorcycleServiceMock.Verify(service => service.GetMotorcycleByIdAsync(query.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundMessage_WhenMotorcycleDoesNotExist()
    {
        var query = new GetMotorcycleByIdQuery { Id = "123" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcycleByIdAsync(query.Id))
                              .ReturnsAsync((MotorcycleService.Domain.Models.Motorcycle?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(Messages.MotorcycleNotFound, result.Content?.GetType().GetProperty("Mensagem")?.GetValue(result.Content));
        _motorcycleServiceMock.Verify(service => service.GetMotorcycleByIdAsync(query.Id), Times.Once);
    }
}