namespace TravelOrganizer.Dtos;

/// <summary>
/// DTO para registrar o actualizar una actividad.
/// </summary>
public record ActivityPostDto(
    int TripId,
    string Title,
    string? Location,
    DateTime StartDateTime,
    DateTime EndDateTime,
    decimal? Cost,
    string? Notes
);

/// <summary>
/// DTO para mostrar información de una actividad.
/// </summary>
public record ActivityGetDto(
    int Id,
    int TripId,
    string Title,
    string? Location,
    DateTime StartDateTime,
    DateTime EndDateTime,
    decimal? Cost,
    string? Notes
);
