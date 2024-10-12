using DeliveryPilots.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryPilots.Infrastructure.Mappings;

public class DeliveryManMap : IEntityTypeConfiguration<DeliveryMan>
{
    public void Configure(EntityTypeBuilder<DeliveryMan> builder)
    {
        
        builder.HasKey(dm => dm.Identificador);

        builder.Property(dm => dm.Nome)
            .HasMaxLength(100);

        builder.Property(dm => dm.Cnpj)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(dm => dm.DataNascimento)
            .IsRequired();

        builder.Property(dm => dm.NumeroCnh)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(dm => dm.TipoCnh)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(dm => dm.Cnpj)
            .IsUnique();

        builder.HasIndex(dm => dm.NumeroCnh)
            .IsUnique();
    }
}