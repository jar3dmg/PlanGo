namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa la información necesaria para crear o actualizar un gasto.
/// </summary>
public class ExpensePostDto
{
    public int TripId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string? Notes { get; set; }
}
