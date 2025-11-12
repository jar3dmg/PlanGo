using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Interfaz que define los métodos de acceso a la base de datos
/// para la entidad Trip (viaje).
/// </summary>
public interface ITripRepository
{
    Task<List<Trip>> GetAllAsync();      // Obtener todos los viajes
    Task<Trip?> GetByIdAsync(int id);    // Obtener un viaje por ID
    Task<Trip> InsertAsync(Trip trip);   // Insertar un nuevo viaje
    Task UpdateAsync(Trip trip);         // Actualizar un viaje existente
    Task DeleteAsync(int id);            // Eliminar un viaje
}
