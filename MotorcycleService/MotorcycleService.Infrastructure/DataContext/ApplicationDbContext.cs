using MotorcycleService.Domain.Models;
using MotorcycleService.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleService.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Motorcycle> Motorcycle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MotorcycleMap());

        base.OnModelCreating(modelBuilder);
    }
}
