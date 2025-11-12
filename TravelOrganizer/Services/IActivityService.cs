using TravelOrganizer.Dtos;

namespace TravelOrganizer.Services;

/// <summary>
/// Define las operaciones de negocio para las actividades.
/// </summary>
public interface IActivityService
{
    Task<List<ActivityGetDto>> GetByTripAsync(int tripId);
    Task<ActivityGetDto?> GetByIdAsync(int id);
    Task<ActivityGetDto> CreateAsync(ActivityPostDto dto);
    Task UpdateAsync(int id, ActivityPostDto dto);
    Task DeleteAsync(int id);
}
