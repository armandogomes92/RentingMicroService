using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalMotorcycle.Domain.Models;

public class DeliveryMan
{
    [Key]
    [Column("identificador")]
    public string Identificador { get; set; }
    [Column("nome")]
    public string Nome { get; set; }
    [Column("cnpj")]
    public string Cnpj { get; set; }
    [Column("data_nascimento")]
    public DateTime DataNascimento { get; set; }
    [Column("numero_cnh")]
    public string NumeroCnh { get; set; }
    [Column("tipo_cnh")]
    public string TipoCnh { get; set; }
}
