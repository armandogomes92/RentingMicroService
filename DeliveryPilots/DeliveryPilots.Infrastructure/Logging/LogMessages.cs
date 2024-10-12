namespace DeliveryPilots.Infrastructure.Logging;

public static class LogMessages
{
    public static string Start(string name) => $"Iniciando o {name}";
    public static string Finished(string name) => $"Finalizando o {name}";
}