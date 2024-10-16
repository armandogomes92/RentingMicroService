using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalMotorcycle.Domain.Models;

public class Rental
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("identificador")]
    public int Identificador { get; set; }
    [Column("entregador_id")]
    public string EntregadorId { get; set; }
    public DeliveryMan Entregador { get; set; }
    [Column("moto_id")]
    public string MotoId { get; set; }
    public Motorcycle Moto { get; set; }
    [Column("data_inicio")]
    private DateTime _dataInicio;
    [Column("data_inicio")]
    public DateTime DataInicio
    {
        get => _dataInicio;
        set => _dataInicio = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime _dataTermino;
    [Column("data_termino")]
    public DateTime DataTermino
    {
        get => _dataTermino;
        set => _dataTermino = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime _dataPrevisaoTermino;
    [Column("data_previsao_termino")]
    public DateTime DataPrevisaoTermino
    {
        get => _dataPrevisaoTermino;
        set => _dataPrevisaoTermino = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime? _dataDevolucao;
    [Column("data_devolucao")]
    public DateTime? DataDevolucao
    {
        get => _dataDevolucao;
        set => _dataDevolucao = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : (DateTime?)null;
    }
    [Column("plano")]
    public int Plano { get; set; }
    [Column("rented")]
    public bool Rented { get; set; }
    [Column("valor_diaria")]
    public float? ValorDiaria { get; set; }
    [Column("valor_total")]
    public decimal? ValorTotal { get; set; }
}
