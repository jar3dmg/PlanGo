using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Data;
using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Implementación del repositorio de viajes usando Entity Framework Core.
/// </summary>
public class TripRepository(AppDbContext db) : ITripRepository
{
    // Obtiene todos los viajes (sin tracking para rendimiento)
    public Task<List<Trip>> GetAllAsync() =>
        db.Trips.AsNoTracking().ToListAsync();

    // Obtiene un viaje por su ID, incluyendo actividades y gastos relacionados
    public Task<Trip?> GetByIdAsync(int id) =>
        db.Trips
          .Include(t => t.Activities)
          .Include(t => t.Expenses)
          .AsNoTracking()
          .FirstOrDefaultAsync(t => t.Id == id);

    // Inserta un nuevo viaje
    public async Task<Trip> InsertAsync(Trip trip)
    {
        db.Trips.Add(trip);
        await db.SaveChangesAsync();
        return trip;
    }

    // Actualiza los datos de un viaje existente
    public async Task UpdateAsync(Trip trip)
    {
        db.Trips.Update(trip);
        await db.SaveChangesAsync();
    }

    // Elimina un viaje por su ID
    public async Task DeleteAsync(int id)
    {
        var entity = await db.Trips.FindAsync(id);
        if (entity is null) return;
        db.Trips.Remove(entity);
        await db.SaveChangesAsync();
    }
}
