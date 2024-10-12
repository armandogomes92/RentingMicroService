using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Resources;

namespace MotorcycleService.Tests.Handlers;

public class UpdateMotorcyclePlateHandlerTests
{
    private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
    private readonly Mock<ILogger<UpdateMotorcyclePlateHandler>> _loggerMock;
    private readonly UpdateMotorcyclePlateHandler _handler;

    public UpdateMotorcyclePlateHandlerTests()
    {
        _motorcycleServiceMock = new Mock<IMotorcycleService>();
        _loggerMock = new Mock<ILogger<UpdateMotorcyclePlateHandler>>();
        _handler = new UpdateMotorcyclePlateHandler(_motorcycleServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenUpdateIsSuccessful()
    {

        var mockMotorcycleService = new Mock<IMotorcycleService>();
        var mockLogger = new Mock<ILogger<UpdateMotorcyclePlateHandler>>();
        var handler = new UpdateMotorcyclePlateHandler(mockMotorcycleService.Object, mockLogger.Object);


        var response = await handler.Handle(new UpdateMotorcycleCommand(), CancellationToken.None);


        Assert.Equal(Messages.UpdatePlate, response.Messagem);
        // Verifique se o método LogInformation foi chamado
    }

    [Fact]
    public async Task Handle_ShouldLogStartAndFinishMessages()
    {

        var command = new UpdateMotorcycleCommand { Identificador = "1", Placa = "ABC1234" };
        _motorcycleServiceMock.Setup(service => service.UpdateMotorcycleByIdAsync(command, CancellationToken.None)).ReturnsAsync(true);


        await _handler.Handle(command, CancellationToken.None);


        _loggerMock.Verify(logger => logger.Log(
        LogLevel.Information,
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(nameof(UpdateMotorcyclePlateHandler)) == true),
        null,
        It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Exactly(2));
    }
}