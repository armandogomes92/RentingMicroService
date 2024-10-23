using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BFFService.Models;

public class Locacao
{
    [Required]
    [JsonPropertyName("data_devolucao")]
    public DateTime DataDevolucao { get; set; }
}