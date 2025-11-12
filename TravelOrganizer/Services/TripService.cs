using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Data;
using TravelOrganizer.Dtos;
using TravelOrganizer.Entities;
using TravelOrganizer.Repositories;

namespace TravelOrganizer.Services;

/// <summary>
/// Implementación de la capa de servicio para manejar la lógica de los viajes.
/// Contiene operaciones CRUD, cálculo de balance y resumen completo.
/// </summary>
public class TripService(ITripRepository repo, AppDbContext db) : ITripService
{
    // ============================================================
    // MÉTODOS CRUD BÁSICOS
    // ============================================================

    public async Task<List<TripGetDto>> GetAllAsync()
    {
        var items = await repo.GetAllAsync();
        return items.Select(t => new TripGetDto(
            t.Id, t.Name, t.Destination, t.StartDate, t.EndDate, t.Budget, t.Notes
        )).ToList();
    }

    public async Task<TripGetDto?> GetByIdAsync(int id)
    {
        var t = await repo.GetByIdAsync(id);
        return t is null ? null : new TripGetDto(
            t.Id, t.Name, t.Destination, t.StartDate, t.EndDate, t.Budget, t.Notes
        );
    }

    public async Task<TripGetDto> CreateAsync(TripPostDto dto)
    {
        var entity = new Trip
        {
            Name = dto.Name,
            Destination = dto.Destination,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Budget = dto.Budget,
            Notes = dto.Notes
        };

        var saved = await repo.InsertAsync(entity);

        return new TripGetDto(
            saved.Id,
            saved.Name,
            saved.Destination,
            saved.StartDate,
            saved.EndDate,
            saved.Budget,
            saved.Notes
        );
    }

    public async Task UpdateAsync(int id, TripPostDto dto)
    {
        var existing = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Viaje no encontrado");

        existing.Name = dto.Name;
        existing.Destination = dto.Destination;
        existing.StartDate = dto.StartDate;
        existing.EndDate = dto.EndDate;
        existing.Budget = dto.Budget;
        existing.Notes = dto.Notes;

        await repo.UpdateAsync(existing);
    }

    public Task DeleteAsync(int id) => repo.DeleteAsync(id);

    // ============================================================
    // MÉTODO: BALANCE FINANCIERO DEL VIAJE
    // ============================================================

    public async Task<TripBalanceDto?> GetTripBalanceAsync(int tripId)
    {
        var trip = await db.Trips.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tripId);

        if (trip is null)
            return null;

        var totalExpenses = await db.Expenses
            .Where(e => e.TripId == tripId)
            .SumAsync(e => (decimal?)e.Amount) ?? 0;

        var remaining = trip.Budget - totalExpenses;

        return new TripBalanceDto(
            trip.Id,
            trip.Name,
            trip.Budget,
            totalExpenses,
            remaining
        );
    }

    // ============================================================
    // MÉTODO: RESUMEN COMPLETO DEL VIAJE
    // ============================================================

    /// <summary>
    /// Devuelve un resumen con los datos del viaje,
    /// sus actividades, sus gastos y el balance financiero.
    /// </summary>
    public async Task<TripSummaryDto?> GetTripSummaryAsync(int tripId)
    {
        // Obtener el viaje
        var trip = await db.Trips.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tripId);

        if (trip is null)
            return null;

        // Obtener actividades del viaje
        var activities = await db.Activities
            .Where(a => a.TripId == tripId)
            .OrderBy(a => a.StartDateTime)
            .Select(a => new ActivityGetDto(
                a.Id,
                a.TripId,
                a.Title,
                a.Location,
                a.StartDateTime,
                a.EndDateTime,
                a.Cost,
                a.Notes
            ))
            .ToListAsync();

        // Obtener gastos del viaje
        var expenses = await db.Expenses
            .Where(e => e.TripId == tripId)
            .OrderBy(e => e.Date)
            .Select(e => new ExpenseGetDto(
                e.Id,
                e.TripId,
                e.Description,
                e.Amount,
                e.Date,
                e.Notes
            ))
            .ToListAsync();

        // Calcular totales
        var totalExpenses = expenses.Sum(e => e.Amount);
        var remaining = trip.Budget - totalExpenses;

        // Retornar resumen
        return new TripSummaryDto(
            trip.Id,
            trip.Name,
            trip.Destination,
            trip.StartDate,
            trip.EndDate,
            trip.Budget,
            activities,
            expenses,
            totalExpenses,
            remaining
        );
    }
}
