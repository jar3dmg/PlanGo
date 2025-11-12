using TravelOrganizer.Dtos;
using TravelOrganizer.Entities;
using TravelOrganizer.Repositories;

namespace TravelOrganizer.Services;

/// <summary>
/// Implementación de la lógica de negocio para los gastos.
/// </summary>
public class ExpenseService(IExpenseRepository repo) : IExpenseService
{
    public async Task<List<ExpenseGetDto>> GetByTripAsync(int tripId)
    {
        var list = await repo.GetByTripAsync(tripId);
        return list.Select(e => new ExpenseGetDto(
            e.Id, e.TripId, e.Description, e.Amount, e.Date, e.Notes
        )).ToList();
    }

    public async Task<ExpenseGetDto?> GetByIdAsync(int id)
    {
        var e = await repo.GetByIdAsync(id);
        return e is null ? null : new ExpenseGetDto(
            e.Id, e.TripId, e.Description, e.Amount, e.Date, e.Notes
        );
    }

    public async Task<ExpenseGetDto> CreateAsync(ExpensePostDto dto)
    {
        var entity = new Expense
        {
            TripId = dto.TripId,
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Notes = dto.Notes
        };

        var saved = await repo.InsertAsync(entity);
        return new ExpenseGetDto(saved.Id, saved.TripId, saved.Description, saved.Amount, saved.Date, saved.Notes);
    }

    public async Task UpdateAsync(int id, ExpensePostDto dto)
    {
        var existing = await repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Gasto no encontrado");
        existing.Description = dto.Description;
        existing.Amount = dto.Amount;
        existing.Date = dto.Date;
        existing.Notes = dto.Notes;
        await repo.UpdateAsync(existing);
    }

    public Task DeleteAsync(int id) => repo.DeleteAsync(id);
}
