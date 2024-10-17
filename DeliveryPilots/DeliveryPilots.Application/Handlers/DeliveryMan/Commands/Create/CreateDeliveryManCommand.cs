using DeliveryPilots.Application.Handlers.CommonResources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;

public class CreateDeliveryManCommand : Command
{
    [Required]
    public string Identificador { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required, MinLength(11), MaxLength(11)]
    public string Cnpj { get; set; }

    [Required, JsonPropertyName("data_nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required, JsonPropertyName("numero_cnh")]
    public string NumeroCnh { get; set; }

    [Required, JsonPropertyName("tipo_cnh")]
    [MaxLength(2), MinLength(1)]
    public string TipoCnh { get; set; }

    [Required, JsonPropertyName("imagem_cnh")]
    public byte[] ImagemCnh { get; set; }
}