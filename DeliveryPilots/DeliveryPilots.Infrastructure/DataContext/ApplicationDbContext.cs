using DeliveryPilots.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryPilots.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<DeliveryMan> DeliveryMan { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryMan>().ToTable("deliverymen");
        base.OnModelCreating(modelBuilder);
    }
}