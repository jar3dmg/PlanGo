using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Interfaz que define las operaciones CRUD para los gastos.
/// </summary>
public interface IExpenseRepository
{
    Task<List<Expense>> GetByTripAsync(int tripId);
    Task<Expense?> GetByIdAsync(int id);
    Task<Expense> InsertAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(int id);
}
