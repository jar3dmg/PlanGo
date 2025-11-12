namespace TravelOrganizer.Dtos;

/// <summary>
/// DTO que representa un resumen completo de un viaje,
/// incluyendo sus actividades, gastos y balance financiero.
/// </summary>
public record TripSummaryDto(
    int TripId,
    string TripName,
    string? Destination,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Budget,
    List<ActivityGetDto> Activities,
    List<ExpenseGetDto> Expenses,
    decimal TotalExpenses,
    decimal RemainingBalance
);
