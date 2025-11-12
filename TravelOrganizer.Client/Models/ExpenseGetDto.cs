namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa un gasto registrado dentro de un viaje.
/// </summary>
public class ExpenseGetDto
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public string? Notes { get; set; }
}
