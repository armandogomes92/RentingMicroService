using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryPilots.Domain.Resources;

public static class Messages
{
    public static string InvalidData = "Dados inv�lidos";
    public static string RegisteredMotorcycle = "Moto cadastrada com sucesso";
    public static string Motorcycle2024 = "Moto � do ano de 2024";
    public static string MotorcycleNotFound = "Moto n�o encontrada";
    public static string UpdatePlate = "Placa modificada com sucesso";
    public static string BadRequest = "Request mal formada";
    public static string IdentificadorExists = "O identificador j� foi cadastrado";
    public static string CnpjExists = "O CNPJ j� foi cadastrado";
    public static string CnhNumberExists = "O n�mero da CNH j� foi cadastrado";
    public static string DeliveryManNotFound = "Entregador n�o encontrado";

    #region validatorMessages
    public static string InvalidId = "O identificador n�o poder ser vazio ou ser menor que 1 caractere";
    public static string InvalidName = "O nome n�o poder ser vazio ou ser menor que 3 caracteres";
    public static string InvalidCnpj = "O CNPJ n�o poder ser vazio ou ter mais de 11 caracteres";
    public static string InvalidBirthDate = "A data de nascimento n�o poder ser vazia";
    public static string InvalidCnhNumber = "O n�mero da CNH � inv�lido";
    public static string InvalidTypeOfCnh = "A categoria Fornecida n�o � v�lida";
    public static string InvalidCnhImage = "A imagem da CNH n�o pode ser vazia ou a extens�o n�o � PNG ou BMP";
    #endregion
}