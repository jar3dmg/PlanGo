using TravelOrganizer.Dtos;

namespace TravelOrganizer.Services;

/// <summary>
/// Define las operaciones de negocio relacionadas con los gastos.
/// </summary>
public interface IExpenseService
{
    Task<List<ExpenseGetDto>> GetByTripAsync(int tripId);
    Task<ExpenseGetDto?> GetByIdAsync(int id);
    Task<ExpenseGetDto> CreateAsync(ExpensePostDto dto);
    Task UpdateAsync(int id, ExpensePostDto dto);
    Task DeleteAsync(int id);
}
