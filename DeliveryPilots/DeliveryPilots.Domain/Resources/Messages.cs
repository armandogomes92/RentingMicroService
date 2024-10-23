using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryPilots.Domain.Resources;

public static class Messages
{
    public static string InvalidData = "Dados inválidos";
    public static string RegisteredMotorcycle = "Moto cadastrada com sucesso";
    public static string Motorcycle2024 = "Moto é do ano de 2024";
    public static string MotorcycleNotFound = "Moto não encontrada";
    public static string UpdatePlate = "Placa modificada com sucesso";
    public static string BadRequest = "Request mal formada";
    public static string IdentificadorExists = "O identificador já foi cadastrado";
    public static string CnpjExists = "O CNPJ já foi cadastrado";
    public static string CnhNumberExists = "O número da CNH já foi cadastrado";
    public static string DeliveryManNotFound = "Entregador não encontrado";

    #region validatorMessages
    public static string InvalidId = "O identificador não poder ser vazio ou ser menor que 1 caractere";
    public static string InvalidName = "O nome não poder ser vazio ou ser menor que 3 caracteres";
    public static string InvalidCnpj = "O CNPJ não poder ser vazio ou ter mais de 11 caracteres";
    public static string InvalidBirthDate = "A data de nascimento não poder ser vazia";
    public static string InvalidCnhNumber = "O número da CNH é inválido";
    public static string InvalidTypeOfCnh = "A categoria Fornecida não é válida";
    public static string InvalidCnhImage = "A imagem da CNH não pode ser vazia ou a extensão não é PNG ou BMP";
    #endregion
}