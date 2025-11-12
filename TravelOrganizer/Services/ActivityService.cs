using TravelOrganizer.Dtos;
using TravelOrganizer.Entities;
using TravelOrganizer.Repositories;

namespace TravelOrganizer.Services;

/// <summary>
/// Lógica de negocio para las actividades.
/// </summary>
public class ActivityService(IActivityRepository repo) : IActivityService
{
    public async Task<List<ActivityGetDto>> GetByTripAsync(int tripId)
    {
        var list = await repo.GetByTripAsync(tripId);
        return list.Select(a => new ActivityGetDto(
            a.Id, a.TripId, a.Title, a.Location,
            a.StartDateTime, a.EndDateTime, a.Cost, a.Notes
        )).ToList();
    }

    public async Task<ActivityGetDto?> GetByIdAsync(int id)
    {
        var a = await repo.GetByIdAsync(id);
        return a is null ? null : new ActivityGetDto(
            a.Id, a.TripId, a.Title, a.Location,
            a.StartDateTime, a.EndDateTime, a.Cost, a.Notes
        );
    }

    public async Task<ActivityGetDto> CreateAsync(ActivityPostDto dto)
    {
        var entity = new Activity
        {
            TripId = dto.TripId,
            Title = dto.Title,
            Location = dto.Location,
            StartDateTime = dto.StartDateTime,
            EndDateTime = dto.EndDateTime,
            Cost = dto.Cost,
            Notes = dto.Notes
        };

        var saved = await repo.InsertAsync(entity);
        return new ActivityGetDto(saved.Id, saved.TripId, saved.Title, saved.Location, saved.StartDateTime, saved.EndDateTime, saved.Cost, saved.Notes);
    }

    public async Task UpdateAsync(int id, ActivityPostDto dto)
    {
        var existing = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Actividad no encontrada");
        existing.Title = dto.Title;
        existing.Location = dto.Location;
        existing.StartDateTime = dto.StartDateTime;
        existing.EndDateTime = dto.EndDateTime;
        existing.Cost = dto.Cost;
        existing.Notes = dto.Notes;

        await repo.UpdateAsync(existing);
    }

    public Task DeleteAsync(int id) => repo.DeleteAsync(id);
}
