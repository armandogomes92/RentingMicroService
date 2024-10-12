using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Interfaces;

namespace MotorcycleService.Tests.Handlers
{
    public class CreateMotorcycleHandlerTests
    {
        private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
        private readonly Mock<ILogger<CreateMotorcycleHandler>> _loggerMock;
        private readonly CreateMotorcycleHandler _handler;

        public CreateMotorcycleHandlerTests()
        {
            _motorcycleServiceMock = new Mock<IMotorcycleService>();
            _loggerMock = new Mock<ILogger<CreateMotorcycleHandler>>();
            _handler = new CreateMotorcycleHandler(_motorcycleServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallCreateMotorcycleAsync()
        {
            var command = new CreateMotorcycleCommand { Identificador = "1", Ano = 2023, Modelo = "Model X", Placa = "ABC-1234" };
            _motorcycleServiceMock.Setup(service => service.CreateMotorcycleAsync(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            _motorcycleServiceMock.Verify(service => service.CreateMotorcycleAsync(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True((bool?)result.Content);
        }

        [Fact]
        public async Task Handle_ShouldReturnCorrectResponse()
        {
            var command = new CreateMotorcycleCommand { Identificador = "1", Ano = 2023, Modelo = "Model X", Placa = "ABC-1234" };
            _motorcycleServiceMock.Setup(service => service.CreateMotorcycleAsync(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True((bool?)result.Content);
        }
    }
}