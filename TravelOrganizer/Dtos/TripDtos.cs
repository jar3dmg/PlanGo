namespace TravelOrganizer.Dtos;

/// <summary>
/// DTO para crear o actualizar un viaje.
/// Contiene únicamente la información que el usuario puede enviar.
/// </summary>
public record TripPostDto(
    string Name,
    string Destination,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Budget,
    string? Notes
);

/// <summary>
/// DTO para mostrar información de un viaje.
/// Es lo que se devuelve al cliente.
/// </summary>
public record TripGetDto(
    int Id,
    string Name,
    string Destination,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Budget,
    string? Notes
);
