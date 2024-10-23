using DeliveryPilots.Domain.Models;
using DeliveryPilots.Infrastructure.DataContext;
using DeliveryPilots.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DeliveryPilots.Tests.Repositories;

public class DeliveryManRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<ILogger<DeliveryManRepository>> _loggerMock;
    private readonly DeliveryManRepository _repository;

    public DeliveryManRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _loggerMock = new Mock<ILogger<DeliveryManRepository>>();
        _repository = new DeliveryManRepository(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task AddDeliveryManAsync_Success()
    {
        // Arrange
        var deliveryMan = new DeliveryMan
        {
            Identificador = "123",
            Nome = "John Doe",
            Cnpj = "12345678901234",
            DataNascimento = new DateTime(1980, 1, 1),
            NumeroCnh = "1234567890",
            TipoCnh = "B"
        };

        // Act
        var result = await _repository.AddDeliveryManAsync(deliveryMan);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task InsertOrUpdateImageCnhAsync_Success()
    {
        // Arrange
        var cnhImage = new byte[] { 0x20 };
        var identificador = "123";
        var directoryPath = Path.Combine("LocalImages", identificador);
        var filePath = Path.Combine(directoryPath, "cnh.jpg");

        // Act
        var result = await _repository.InsertOrUpdateImageCnhAsync(cnhImage, identificador);

        // Assert
        Assert.True(result);
        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public async Task GetCnhCategory_ReturnsCategory()
    {
        // Arrange
        var identificador = "124";
        var expectedCategory = "B";
        var deliveryMan = new DeliveryMan
        {
            Identificador = identificador,
            Nome = "John Doe",
            Cnpj = "12345678901234",
            DataNascimento = new DateTime(1980, 1, 1),
            NumeroCnh = "1234567890",
            TipoCnh = expectedCategory
        };
        _context.DeliveryMan.Add(deliveryMan);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetCnhCategory(identificador);

        // Assert
        Assert.Equal(expectedCategory, result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanById_ReturnsTrue()
    {
        // Arrange
        var id = "125";
        var deliveryMan = new DeliveryMan
        {
            Identificador = id,
            Nome = "John Doe",
            Cnpj = "12345678901234",
            DataNascimento = new DateTime(1980, 1, 1),
            NumeroCnh = "1234567890",
            TipoCnh = "B"
        };
        _context.DeliveryMan.Add(deliveryMan);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CheckIfExistDeliverymanById(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanByCnpj_ReturnsTrue()
    {
        // Arrange
        var cnpj = "456";
        var deliveryMan = new DeliveryMan
        {
            Identificador = "126",
            Nome = "John Doe",
            Cnpj = cnpj,
            DataNascimento = new DateTime(1980, 1, 1),
            NumeroCnh = "1234567890",
            TipoCnh = "B"
        };
        _context.DeliveryMan.Add(deliveryMan);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CheckIfExistDeliverymanByCnpj(cnpj);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckIfExistDeliverymanByCnhNumber_ReturnsTrue()
    {
        // Arrange
        var cnhNumber = "789";
        var deliveryMan = new DeliveryMan
        {
            Identificador = "127",
            Nome = "John Doe",
            Cnpj = "12345678901234",
            DataNascimento = new DateTime(1980, 1, 1),
            NumeroCnh = cnhNumber,
            TipoCnh = "B"
        };
        _context.DeliveryMan.Add(deliveryMan);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CheckIfExistDeliverymanByCnhNumber(cnhNumber);

        // Assert
        Assert.True(result);
    }
}