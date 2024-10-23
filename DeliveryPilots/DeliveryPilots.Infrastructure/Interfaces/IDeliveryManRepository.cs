using DeliveryPilots.Domain.Models;

namespace DeliveryPilots.Infrastructure.Interfaces;

public interface IDeliveryManRepository
{
    Task<bool> AddDeliveryManAsync(DeliveryMan deliveryMan);
    Task<bool> InsertOrUpdateImageCnhAsync(byte[] cngImage, string identificador);
    Task<string> GetCnhCategory(string identificador);
    Task<bool> CheckIfExistDeliverymanById(string id);
    Task<bool> CheckIfExistDeliverymanByCnpj(string cnpj);
    Task<bool> CheckIfExistDeliverymanByCnhNumber(string cnh);
}