using DeliveryPilots.Domain.Models;

namespace DeliveryPilots.Infrastructure.Interfaces;

public interface IDeliveryManRepository
{
    Task<bool> AddDeliveryManAsync(DeliveryMan deliveryMan);
    Task<bool> InsertOrUpdateImageCnhAsync(byte[] cngImage, string identificador);
    Task<string> GetCnhCategory(string identificador);
}