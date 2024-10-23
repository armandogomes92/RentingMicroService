using DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;
using DeliveryPilots.Application.Interfaces;
using DeliveryPilots.Domain.Resources;
using Microsoft.Extensions.Logging;
using Moq;

namespace DeliveryPilots.Tests.Handlers;

public class CreateDeliveryManHandlerTests
{
    private readonly Mock<ILogger<CreateDeliveryManHandler>> _loggerMock;
    private readonly Mock<IDeliveryManService> _deliveryManServiceMock;
    private readonly CreateDeliveryManHandler _handler;

    public CreateDeliveryManHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateDeliveryManHandler>>();
        _deliveryManServiceMock = new Mock<IDeliveryManService>();
        _handler = new CreateDeliveryManHandler(_loggerMock.Object, _deliveryManServiceMock.Object);
    }

    [Fact]
    public async Task Handle_IdentificadorExists_ReturnsIdentificadorExistsMessage()
    {
        var command = new CreateDeliveryManCommand { Identificador = "123", Cnpj = "456", NumeroCnh = "789" };
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response.Content);
        Assert.Contains(Messages.IdentificadorExists, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_CnpjExists_ReturnsCnpjExistsMessage()
    {
        var command = new CreateDeliveryManCommand { Identificador = "123", Cnpj = "456", NumeroCnh = "789" };
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanByCnpj(command.Cnpj)).ReturnsAsync(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response.Content);
        Assert.Contains(Messages.CnpjExists, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_CnhExists_ReturnsCnhNumberExistsMessage()
    {
        var command = new CreateDeliveryManCommand { Identificador = "123", Cnpj = "456", NumeroCnh = "789" };
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanByCnpj(command.Cnpj)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanByCnhNumber(command.NumeroCnh)).ReturnsAsync(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response.Content);
        Assert.Contains(Messages.CnhNumberExists, response.Content.ToString());
    }

    [Fact]
    public async Task Handle_NoConflicts_CreatesDeliveryMan()
    {
        var command = new CreateDeliveryManCommand { Identificador = "123", Cnpj = "456", NumeroCnh = "789" };
        var expectedResult = true;
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanById(command.Identificador)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanByCnpj(command.Cnpj)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CheckIfExistDeliverymanByCnhNumber(command.NumeroCnh)).ReturnsAsync(false);
        _deliveryManServiceMock.Setup(s => s.CreateDeliveryMan(command)).ReturnsAsync(expectedResult);

        var response = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(expectedResult, response.Content);
    }
}