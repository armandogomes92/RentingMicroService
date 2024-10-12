using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleService.Application.Handlers.Motorcycle.Queries;
using MotorcycleService.Application.Interfaces;
using MotorcycleService.Domain.Models;

namespace MotorcycleService.Tests.Handlers
{
    public class GetMotorcyclesHandlerTests
    {
        private readonly Mock<IMotorcycleService> _motorcycleServiceMock;
        private readonly Mock<ILogger<GetMotorcyclesHandler>> _loggerMock;
        private readonly GetMotorcyclesHandler _handler;

        public GetMotorcyclesHandlerTests()
        {
            _motorcycleServiceMock = new Mock<IMotorcycleService>();
            _loggerMock = new Mock<ILogger<GetMotorcyclesHandler>>();
            _handler = new GetMotorcyclesHandler(_motorcycleServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMotorcycles_WhenMotorcyclesExist()
        {

            var query = new GetMotorcyclesQuery();
            var motorcycles = new List<Motorcycle>
            {
                new Motorcycle { Identificador = "123", Ano = 2020, Modelo = "Model A", Placa = "ABC-1234" },
                new Motorcycle { Identificador = "124", Ano = 2021, Modelo = "Model B", Placa = "DEF-5678" }
            };
            _motorcycleServiceMock.Setup(service => service.GetMotorcyclesAsync())
                                  .ReturnsAsync(motorcycles);


            var result = await _handler.Handle(query, CancellationToken.None);


            Assert.NotNull(result);
            Assert.Equal(motorcycles, result.Content);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMotorcyclesExist()
        {

            var query = new GetMotorcyclesQuery();
            var motorcycles = new List<Motorcycle>();
            _motorcycleServiceMock.Setup(service => service.GetMotorcyclesAsync())
                                  .ReturnsAsync(motorcycles);


            var result = await _handler.Handle(query, CancellationToken.None);


            Assert.NotNull(result);
            Assert.Empty((System.Collections.IEnumerable)result.Content);
            _motorcycleServiceMock.Verify(service => service.GetMotorcyclesAsync(), Times.Once);
        }
    }
}