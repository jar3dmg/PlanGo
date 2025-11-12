using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Entities;

namespace TravelOrganizer.Data;

/// <summary>
/// Contexto principal de la base de datos.
/// Se encarga de mapear las entidades (clases) a tablas dentro de SQLite.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Tablas principales
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Expense> Expenses => Set<Expense>();

    /// <summary>
    /// Configura los modelos (restricciones, relaciones y tipos de datos).
    /// </summary>
    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Configuración de la tabla Trip (Viajes)
        mb.Entity<Trip>(e =>
        {
            e.Property(p => p.Name).HasMaxLength(100).IsRequired();
            e.Property(p => p.Destination).HasMaxLength(100);
            e.Property(p => p.Budget).HasColumnType("decimal(18,2)");

            // Un viaje tiene muchas actividades
            e.HasMany(t => t.Activities)
             .WithOne(a => a.Trip!)
             .HasForeignKey(a => a.TripId)
             .OnDelete(DeleteBehavior.Cascade);

            // Un viaje tiene muchos gastos
            e.HasMany(t => t.Expenses)
             .WithOne(x => x.Trip!)
             .HasForeignKey(x => x.TripId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de la tabla Activity (Actividades)
        mb.Entity<Activity>(e =>
        {
            e.Property(p => p.Title).HasMaxLength(120).IsRequired();
            e.Property(p => p.Cost).HasColumnType("decimal(18,2)");
        });

        // Configuración de la tabla Expense (Gastos)
        mb.Entity<Expense>(e =>
        {
            e.Property(p => p.Description).HasMaxLength(200).IsRequired();
            e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        });
    }
}
