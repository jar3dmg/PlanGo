using System.Diagnostics;

namespace TravelOrganizer.Entities;

/// <summary>
/// Representa un viaje que puede tener actividades y gastos.
/// </summary>
public class Trip
{
    public int Id { get; set; }                     // Identificador del viaje
    public string Name { get; set; } = "";          // Nombre del viaje
    public string Destination { get; set; } = "";   // Lugar o destino principal
    public DateOnly StartDate { get; set; }         // Fecha de inicio del viaje
    public DateOnly EndDate { get; set; }           // Fecha de fin del viaje
    public decimal Budget { get; set; }             // Presupuesto total
    public string? Notes { get; set; }              // Notas adicionales

    // Relaciones
    public List<Activity> Activities { get; set; } = new();  // Lista de actividades del viaje
    public List<Expense> Expenses { get; set; } = new();     // Lista de gastos del viaje
}
