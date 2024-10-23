using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Resources;

namespace MotorcycleService.Tests.Handlers;

public class UpdateMotorcyclePlateHandlerTests
{
    private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
    private readonly UpdateMotorcyclePlateHandler _handler;

    public UpdateMotorcyclePlateHandlerTests()
    {
        _motorcycleServiceMock = new Mock<IMotorcycleService>();
        _handler = new UpdateMotorcyclePlateHandler(_motorcycleServiceMock.Object, Mock.Of<ILogger<UpdateMotorcyclePlateHandler>>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenUpdateIsSuccessful()
    {
        var command = new UpdateMotorcycleCommand { Identificador = "1", Placa = "ABC1234" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcyclesByPlateAsync(command.Placa))
                              .ReturnsAsync((MotorcycleService.Domain.Models.Motorcycle?)null);
        _motorcycleServiceMock.Setup(service => service.UpdateMotorcycleByIdAsync(command, CancellationToken.None))
                              .ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True((bool)result.Content!);
        _motorcycleServiceMock.Verify(service => service.GetMotorcyclesByPlateAsync(command.Placa), Times.Once);
        _motorcycleServiceMock.Verify(service => service.UpdateMotorcycleByIdAsync(command, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnPlateAlreadyHasRegistrationMessage_WhenPlateExists()
    {
        var command = new UpdateMotorcycleCommand { Identificador = "1", Placa = "ABC1234" };
        var existingMotorcycle = new MotorcycleService.Domain.Models.Motorcycle { Identificador = "2", Placa = "ABC1234" };
        _motorcycleServiceMock.Setup(service => service.GetMotorcyclesByPlateAsync(command.Placa))
                              .ReturnsAsync(existingMotorcycle);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(Messages.PlateAlreadyHasRegistration, result.Content?.GetType().GetProperty("Messagem")?.GetValue(result.Content));
        _motorcycleServiceMock.Verify(service => service.GetMotorcyclesByPlateAsync(command.Placa), Times.Once);
        _motorcycleServiceMock.Verify(service => service.UpdateMotorcycleByIdAsync(It.IsAny<UpdateMotorcycleCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}