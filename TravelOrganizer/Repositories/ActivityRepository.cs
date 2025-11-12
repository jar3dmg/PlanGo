using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Data;
using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Implementación del repositorio de actividades usando Entity Framework.
/// </summary>
public class ActivityRepository(AppDbContext db) : IActivityRepository
{
    public Task<List<Activity>> GetByTripAsync(int tripId) =>
        db.Activities
          .AsNoTracking()
          .Where(a => a.TripId == tripId)
          .OrderBy(a => a.StartDateTime)
          .ToListAsync();

    public Task<Activity?> GetByIdAsync(int id) =>
        db.Activities.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Activity> InsertAsync(Activity activity)
    {
        db.Activities.Add(activity);
        await db.SaveChangesAsync();
        return activity;
    }

    public async Task UpdateAsync(Activity activity)
    {
        db.Activities.Update(activity);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await db.Activities.FindAsync(id);
        if (entity is null) return;
        db.Activities.Remove(entity);
        await db.SaveChangesAsync();
    }
}
