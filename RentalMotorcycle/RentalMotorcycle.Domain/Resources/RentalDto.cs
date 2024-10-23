using RentalMotorcycle.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalMotorcycle.Domain.Resources;

public record RentalDto
{
    [Column("entregador_id")]
    public string EntregadorId { get; set; }

    [Column("moto_id")]
    public string MotoId { get; set; }

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

    public RentalDto(Rental dto)
    {
        EntregadorId = dto.EntregadorId;
        MotoId = dto.MotoId;
        DataInicio = DateTime.SpecifyKind(dto.DataInicio, DateTimeKind.Utc);
        DataTermino = DateTime.SpecifyKind(dto.DataTermino, DateTimeKind.Utc);
        DataPrevisaoTermino = DateTime.SpecifyKind(dto.DataPrevisaoTermino, DateTimeKind.Utc);
        Plano = dto.Plano;
    }
}
