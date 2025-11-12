namespace TravelOrganizer.Entities;

/// <summary>
/// Representa un gasto dentro de un viaje.
/// </summary>
public class Expense
{
    public int Id { get; set; }                     // Identificador del gasto
    public int TripId { get; set; }                 // Relación con el viaje
    public string Description { get; set; } = "";   // Descripción del gasto
    public decimal Amount { get; set; }             // Monto gastado
    public DateOnly Date { get; set; }              // Fecha en la que ocurrió
    public string? Notes { get; set; }              // Notas opcionales sobre el gasto

    // Relación con el viaje principal
    public Trip? Trip { get; set; }
}
