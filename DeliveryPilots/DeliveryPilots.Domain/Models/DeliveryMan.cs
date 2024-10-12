namespace DeliveryPilots.Domain.Models;

public class DeliveryMan
{
    public string Identificador { get; set; }
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTime DataNascimento { get; set; }
    public string NumeroCnh { get; set; }
    public string TipoCnh { get; set; }
}
