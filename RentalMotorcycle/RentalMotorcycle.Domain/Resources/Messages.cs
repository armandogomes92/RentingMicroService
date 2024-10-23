namespace RentalMotorcycle.Domain.Resources;

public static class Messages
{
    public static string InvalidData = "Dados inválidos";
    public static string RegisteredMotorcycle = "Moto cadastrada com sucesso";
    public static string Motorcycle2024 = "Moto é do ano de 2024";
    public static string MotorcycleNotFound = "Moto não encontrada";
    public static string UpdatePlate = "Placa modificada com sucesso";
    public static string BadRequest = "Request mal formada";
    public static string MotorcycleRentRegistryNotFound = "Locação não encontrada";
    public static string ReturnedDate = "Data de devolução informada com sucesso";
    public static string InvalidCnh = "Categoria da CNH inválida";
    public static string MotorcycleIsRenting = "Moto está alugada";

    #region Validator Messages
    public static string InvalidDeliveryManId = "Identificador do entregador inválido";
    public static string InvalidMotorcycleId = "Identificador da moto inválido";
    public static string InvalidStartDate = "Data de início inválida";
    public static string InvalidEndDate = "Data de término inválida";
    public static string InvalidExpectedEndDate = "Data de previsão de término inválida";
    public static string PlanUnavaliable = "Plano de locação inválido";
    #endregion
}