using Amazon.S3;
using Amazon.S3.Model;
using DeliveryPilots.Domain.Models;
using DeliveryPilots.Domain.Resources;
using DeliveryPilots.Infrastructure.DataContext;
using DeliveryPilots.Infrastructure.Interfaces;
using DeliveryPilots.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace DeliveryPilots.Infrastructure.Repositories;

public class DeliveryManRepository : IDeliveryManRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DeliveryManRepository> _logger;
    private readonly IAmazonS3 _s3Client;

    private const string NameOfClass = nameof(DeliveryManRepository);

    public DeliveryManRepository(ApplicationDbContext context, ILogger<DeliveryManRepository> logger, IAmazonS3 s3Client)
    {
        _context = context;
        _logger = logger;
        _s3Client = s3Client;
    }

    public async Task<bool> AddDeliveryManAsync(DeliveryMan deliveryMan)
    {
        var nameForLog = $"{NameOfClass} {nameof(AddDeliveryManAsync)}";

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        await _context.DeliveryMan.AddAsync(deliveryMan);

        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> InsertOrUpdateImageCnhAsync(byte[] cngImage, string identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(InsertOrUpdateImageCnhAsync)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var key = $"{identificador}/cnh.jpg";
        using (var stream = new MemoryStream(cngImage))
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = "CNH",
                Key = key,
                InputStream = stream,
                ContentType = "image/jpeg"
            };

            var response = await _s3Client.PutObjectAsync(putRequest);
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.LogError(LogMessages.Finished(nameForLog));
                return false;
            }
        }
        _logger.LogInformation(LogMessages.Finished(nameForLog));
        return true;
    }

    public async Task<string> GetCnhCategory(string identificador)
    {
        var nameForLog = $"{NameOfClass} {nameof(GetCnhCategory)}";

        _logger.LogInformation(LogMessages.Start(nameForLog));

        var deliveryMan = await _context.DeliveryMan.FindAsync(identificador);

        if (deliveryMan == null)
        {
            _logger.LogError(LogMessages.Finished(nameForLog));
            return Messages.InvalidData;
        }
        _logger.LogInformation(LogMessages.Finished(nameForLog));

        return deliveryMan.TipoCnh;
    }
}