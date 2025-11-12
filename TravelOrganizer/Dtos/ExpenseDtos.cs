namespace TravelOrganizer.Dtos;

/// <summary>
/// DTO para crear o actualizar un gasto.
/// Contiene los campos necesarios para registrar un gasto dentro de un viaje.
/// </summary>
public record ExpensePostDto(
    int TripId,
    string Description,
    decimal Amount,
    DateOnly Date,
    string? Notes
);

/// <summary>
/// DTO para mostrar información de un gasto.
/// </summary>
public record ExpenseGetDto(
    int Id,
    int TripId,
    string Description,
    decimal Amount,
    DateOnly Date,
    string? Notes
);
