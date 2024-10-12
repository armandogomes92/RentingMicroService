using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Resources;

namespace MotorcycleService.Tests.Handlers;

public class GetMotorcycleByIdHandlerTests
{
    private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
    private readonly Mock<ILogger<GetMotorcycleByIdHandler>> _loggerMock;
    private readonly GetMotorcycleByIdHandler _handler;

    public GetMotorcycleByIdHandlerTests()
    {
        _motorcycleServiceMock = new Mock<IMotorcycleService>();
        _loggerMock = new Mock<ILogger<GetMotorcycleByIdHandler>>();
        _handler = new GetMotorcycleByIdHandler(_motorcycleServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMotorcycle_WhenMotorcycleExists()
    {

        var query = new GetMotorcycleByIdQuery { Id = "123" };
        var motorcycle = new Domain.Models.Motorcycle { Identificador = "123", Placa = "ABC-1234" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcycleByIdAsync(query))
                              .ReturnsAsync(motorcycle);


        var result = await _handler.Handle(query, CancellationToken.None);


        Assert.NotNull(result);
        Assert.Equal(motorcycle, result.Content);
        _motorcycleServiceMock.Verify(service => service.GetMotorcycleByIdAsync(query), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundMessage_WhenMotorcycleDoesNotExist()
    {

        var query = new GetMotorcycleByIdQuery { Id = "123" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcycleByIdAsync(query))
                              .ReturnsAsync((Domain.Models.Motorcycle)null);


        var result = await _handler.Handle(query, CancellationToken.None);


        Assert.NotNull(result);
        Assert.Equal(Messages.MotorcycleNotFound, result.Messagem);
        _motorcycleServiceMock.Verify(service => service.GetMotorcycleByIdAsync(query), Times.Once);
    }


    [Fact]
    public async Task Handle_ShouldLogStartAndFinishMessages()
    {

        var query = new GetMotorcycleByIdQuery { Id = "123" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcycleByIdAsync(query))
                              .ReturnsAsync((Domain.Models.Motorcycle)null);


        await _handler.Handle(query, CancellationToken.None);


        _loggerMock.Verify(logger => logger.Log(
        LogLevel.Information,
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(nameof(GetMotorcycleByIdHandler)) == true),
        null,
        It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(2));
    }
}