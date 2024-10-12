using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentalMotorcycle.Domain.Models;

public class Rental
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identificador { get; set; }
    public string EntregadorId { get; set; }
    public string MotoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public DateTime DataPrevisaoTermino { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public int Plano { get; set; }
    public bool Rented { get; set; }
    public float? ValorDiaria { get; set; }
    public decimal? ValorTotal { get; set; }
}
