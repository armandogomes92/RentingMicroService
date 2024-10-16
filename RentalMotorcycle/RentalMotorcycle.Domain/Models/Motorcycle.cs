using System.ComponentModel.DataAnnotations.Schema;

namespace RentalMotorcycle.Domain.Models;

public class Motorcycle
{
    [Column("identificador")]
    public string Identificador { get; set; }
    [Column("ano")]
    public int Ano { get; set; }
    [Column("modelo")]
    public string Modelo { get; set; }
    [Column("placa")]
    public string Placa { get; set; }
}
