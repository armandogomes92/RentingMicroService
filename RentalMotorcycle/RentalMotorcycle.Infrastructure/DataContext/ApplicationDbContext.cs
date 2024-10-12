using Microsoft.EntityFrameworkCore;
using RentalMotorcycle.Domain.Models;

namespace RentalMotorcycle.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Rental> Rental { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rental>()
            .HasKey(r => r.Identificador);
        modelBuilder.Entity<Rental>()
            .Property(r => r.Identificador)
            .ValueGeneratedOnAdd();
    }
}