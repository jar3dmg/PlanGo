namespace TravelOrganizer.Dtos;

/// <summary>
/// DTO que representa el balance financiero de un viaje.
/// </summary>
public record TripBalanceDto(
    int TripId,
    string TripName,
    decimal Budget,
    decimal TotalExpenses,
    decimal RemainingBalance
);
