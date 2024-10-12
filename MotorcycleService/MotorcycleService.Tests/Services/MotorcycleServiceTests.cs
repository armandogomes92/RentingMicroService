using MotorcycleService.Application.Handlers.Motorcycle.Commands.Create;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;
using MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Models;
using MotorcycleService.Domain.Resources;
using MotorcycleService.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace MotorcycleService.Tests.Services
{
    public class MotorcycleServiceTests
    {
        private readonly Mock<IMotorcycleRepository> _motoRepositoryMock;
        private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
        private readonly Mock<ILogger<UpdateMotorcyclePlateHandler>> _loggerMock;
        private readonly IMotorcycleService _motorcycleService;
        private readonly Mock<IRentalService> _rentalServiceMock;

        public MotorcycleServiceTests(Mock<IRentalService> rentalServiceMock)
        {
            _motoRepositoryMock = new Mock<IMotorcycleRepository>();
            _rabbitMqServiceMock = new Mock<IRabbitMqService>();
            _loggerMock = new Mock<ILogger<UpdateMotorcyclePlateHandler>>();
            _motorcycleService = new Application.Services.MotorcycleService(_motoRepositoryMock.Object, _rabbitMqServiceMock.Object, _loggerMock.Object);
            _rentalServiceMock = rentalServiceMock;
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldReturnTrue_WhenMotorcycleIsCreated()
        {
            var command = new CreateMotorcycleCommand { Identificador = "1", Ano = 2023, Modelo = "Model X", Placa = "ABC-1234" };
            _motoRepositoryMock.Setup(repo => repo.AddMotorcycleAsync(It.IsAny<Motorcycle>())).ReturnsAsync(true);

            var result = await _motorcycleService.CreateMotorcycleAsync(command, CancellationToken.None);

            Assert.True(result);
            _rabbitMqServiceMock.Verify(rabbit => rabbit.PublishRegisteredMotorcycle(Messages.RegisteredMotorcycle), Times.Once);
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldReturnFalse_WhenMotorcycleIsNotCreated()
        {
            var command = new CreateMotorcycleCommand { Identificador = "1", Ano = 2023, Modelo = "Model X", Placa = "ABC-1234" };
            _motoRepositoryMock.Setup(repo => repo.AddMotorcycleAsync(It.IsAny<Motorcycle>())).ReturnsAsync(false);

            var result = await _motorcycleService.CreateMotorcycleAsync(command, CancellationToken.None);

            Assert.False(result);
            _rabbitMqServiceMock.Verify(rabbit => rabbit.PublishRegisteredMotorcycle(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetMotorcyclesAsync_ShouldReturnAllMotorcycles()
        {
            var motorcycles = new List<Motorcycle> { new Motorcycle(), new Motorcycle() };
            _motoRepositoryMock.Setup(repo => repo.GetAllMotorcyclesAsync()).ReturnsAsync(motorcycles);

            var result = await _motorcycleService.GetMotorcyclesAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateMotorcycleByIdAsync_ShouldReturnTrue_WhenMotorcycleIsUpdated()
        {
            var command = new UpdateMotorcycleCommand { Identificador = "1", Placa = "XYZ-9876" };
            var existingMoto = new Motorcycle { Identificador = command.Identificador, Placa = "ABC-1234" };
            _motoRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync(command.Identificador)).ReturnsAsync(existingMoto);
            _motoRepositoryMock.Setup(repo => repo.UpdateMotorcycleByIdAsync(existingMoto)).ReturnsAsync(true);

            var result = await _motorcycleService.UpdateMotorcycleByIdAsync(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateMotorcycleByIdAsync_ShouldReturnFalse_WhenMotorcycleIsNotFound()
        {
            var command = new UpdateMotorcycleCommand { Identificador = "1", Placa = "XYZ-9876" };
            _motoRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync(command.Identificador)).ReturnsAsync((Motorcycle?)null);

            var result = await _motorcycleService.UpdateMotorcycleByIdAsync(command, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task GetMotorcycleByIdAsync_ShouldReturnMotorcycle_WhenMotorcycleExists()
        {
            var query = new GetMotorcycleByIdQuery { Id = "1" };
            var motorcycle = new Motorcycle { Identificador = query.Id };
            _motoRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync(query.Id)).ReturnsAsync(motorcycle);

            var result = await _motorcycleService.GetMotorcycleByIdAsync(query);

            Assert.Equal(motorcycle, result);
        }

        [Fact]
        public async Task GetMotorcycleByIdAsync_ShouldReturnNull_WhenMotorcycleDoesNotExist()
        {
            var query = new GetMotorcycleByIdQuery { Id = "1" };
            _motoRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync(query.Id)).ReturnsAsync((Motorcycle?)null);

            var result = await _motorcycleService.GetMotorcycleByIdAsync(query);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteMotorcycleByIdAsync_ShouldReturnFalse_WhenMotorcycleIsNotFound()
        {
            var command = new DeleteMotorcycleByIdCommand { Id = "1" };
            _motoRepositoryMock.Setup(repo => repo.GetMotorcycleByIdAsync(command.Id)).ReturnsAsync((Motorcycle?)null);

            var result = await _motorcycleService.DeleteMotorcycleByIdAsync(command, CancellationToken.None);

            Assert.False(result);
        }
    }
}