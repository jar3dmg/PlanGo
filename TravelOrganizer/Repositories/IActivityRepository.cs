using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Interfaz que define las operaciones CRUD para las actividades.
/// </summary>
public interface IActivityRepository
{
    Task<List<Activity>> GetByTripAsync(int tripId);
    Task<Activity?> GetByIdAsync(int id);
    Task<Activity> InsertAsync(Activity activity);
    Task UpdateAsync(Activity activity);
    Task DeleteAsync(int id);
}
