using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleService.Domain.Models;

namespace MotorcycleService.Infrastructure.Mappings;

public class MotorcycleMap : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(e => e.Identificador);
        builder.Property(e => e.Ano).IsRequired().HasMaxLength(4);
        builder.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Placa).IsRequired().HasMaxLength(10);
    }
}