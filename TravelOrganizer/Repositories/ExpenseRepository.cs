using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Data;
using TravelOrganizer.Entities;

namespace TravelOrganizer.Repositories;

/// <summary>
/// Implementación del repositorio de gastos usando Entity Framework.
/// </summary>
public class ExpenseRepository(AppDbContext db) : IExpenseRepository
{
    // Obtiene todos los gastos de un viaje
    public Task<List<Expense>> GetByTripAsync(int tripId) =>
        db.Expenses
          .AsNoTracking()
          .Where(e => e.TripId == tripId)
          .OrderBy(e => e.Date)
          .ToListAsync();

    // Obtiene un gasto por su ID
    public Task<Expense?> GetByIdAsync(int id) =>
        db.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    // Inserta un nuevo gasto
    public async Task<Expense> InsertAsync(Expense expense)
    {
        db.Expenses.Add(expense);
        await db.SaveChangesAsync();
        return expense;
    }

    // Actualiza un gasto existente
    public async Task UpdateAsync(Expense expense)
    {
        db.Expenses.Update(expense);
        await db.SaveChangesAsync();
    }

    // Elimina un gasto
    public async Task DeleteAsync(int id)
    {
        var entity = await db.Expenses.FindAsync(id);
        if (entity is null) return;
        db.Expenses.Remove(entity);
        await db.SaveChangesAsync();
    }
}
