using System;
using System.Collections.Generic;

namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa el resumen completo de un viaje,
/// incluyendo actividades, gastos y balance final.
/// </summary>
public class TripSummaryDto
{
    public int TripId { get; set; }
    public string TripName { get; set; } = string.Empty;
    public string? Destination { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Budget { get; set; }

    // Listado de actividades asociadas al viaje
    public List<ActivityGetDto> Activities { get; set; } = new();

    // Listado de gastos asociados al viaje
    public List<ExpenseGetDto> Expenses { get; set; } = new();

    // Total de todos los gastos
    public decimal TotalExpenses { get; set; }

    // Diferencia entre el presupuesto y los gastos
    public decimal RemainingBalance { get; set; }
}
