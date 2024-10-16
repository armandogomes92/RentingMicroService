using Microsoft.EntityFrameworkCore;
using RentalMotorcycle.Domain.Models;

namespace RentalMotorcycle.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Rental> Rental { get; set; }
    public virtual DbSet<DeliveryMan> Entregador { get; set; }
    public virtual DbSet<Motorcycle> Moto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rental>().ToTable("rentals");
        modelBuilder.Entity<Rental>()
            .HasKey(r => r.Identificador);
        modelBuilder.Entity<Rental>()
            .Property(r => r.Identificador)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Entregador)
            .WithMany()
            .HasForeignKey(r => r.EntregadorId);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Moto)
            .WithMany()
            .HasForeignKey(r => r.MotoId);

        modelBuilder.Entity<DeliveryMan>().ToTable("deliverymen");
        modelBuilder.Entity<DeliveryMan>()
            .HasKey(r => r.Identificador);

        modelBuilder.Entity<Motorcycle>().ToTable("motorcycles");
        modelBuilder.Entity<Motorcycle>()
            .HasKey(r => r.Identificador);
    }
}