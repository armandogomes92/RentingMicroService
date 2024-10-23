using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Tests.Handlers;

public class DeleteMotorcycleByIdHandlerTests
{
    private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
    private readonly Mock<ILogger<DeleteMotorcycleByIdHandler>> _loggerMock;
    private readonly DeleteMotorcycleByIdHandler _handler;

    public DeleteMotorcycleByIdHandlerTests()
    {
        _motorcycleServiceMock = new Mock<IMotorcycleService>();
        _loggerMock = new Mock<ILogger<DeleteMotorcycleByIdHandler>>();
        _handler = new DeleteMotorcycleByIdHandler(_motorcycleServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallDeleteMotorcycleByIdAsync()
    {
        var command = new DeleteMotorcycleByIdCommand { Id = "1" };
        _motorcycleServiceMock.Setup(service => service.DeleteMotorcycleByIdAsync(command, CancellationToken.None)).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        _motorcycleServiceMock.Verify(service => service.DeleteMotorcycleByIdAsync(command, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenDeletionIsSuccessful()
    {
        var command = new DeleteMotorcycleByIdCommand { Id = "1" };
        _motorcycleServiceMock.Setup(service => service.DeleteMotorcycleByIdAsync(command, CancellationToken.None)).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True((bool)result.Content!);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorResponse_WhenDeletionFails()
    {
        var command = new DeleteMotorcycleByIdCommand { Id = "1" };
        _motorcycleServiceMock.Setup(service => service.DeleteMotorcycleByIdAsync(command, CancellationToken.None)).ReturnsAsync(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False((bool)result.Content!);
    }

    [Fact]
    public async Task Handle_ShouldLogInformation()
    {
        var command = new DeleteMotorcycleByIdCommand { Id = "1" };
        _motorcycleServiceMock.Setup(service => service.DeleteMotorcycleByIdAsync(command, CancellationToken.None)).ReturnsAsync(true);

        await _handler.Handle(command, CancellationToken.None);

        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(nameof(DeleteMotorcycleByIdHandler))),
            null,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(2));
    }
}