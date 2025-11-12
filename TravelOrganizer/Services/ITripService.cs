using TravelOrganizer.Dtos;

namespace TravelOrganizer.Services;

/// <summary>
/// Interfaz que define las operaciones del servicio de viajes.
/// </summary>
public interface ITripService
{
    // CRUD básico
    Task<List<TripGetDto>> GetAllAsync();
    Task<TripGetDto?> GetByIdAsync(int id);
    Task<TripGetDto> CreateAsync(TripPostDto dto);
    Task UpdateAsync(int id, TripPostDto dto);
    Task DeleteAsync(int id);

    // Balance financiero
    Task<TripBalanceDto?> GetTripBalanceAsync(int tripId);

    // Resumen completo
    Task<TripSummaryDto?> GetTripSummaryAsync(int tripId);
}
