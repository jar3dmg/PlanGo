namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa los datos necesarios para crear un nuevo viaje
/// desde el frontend Blazor hacia la API.
/// </summary>
public class TripPostDto
{
    // Nombre del viaje
    public string Name { get; set; } = string.Empty;

    // Destino o lugar del viaje
    public string? Destination { get; set; }

    // Fecha de inicio del viaje (inicializa con la fecha actual)
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    // Fecha de fin del viaje (inicializa con el día siguiente)
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

    // Presupuesto estimado
    public decimal Budget { get; set; }

    // Notas adicionales
    public string? Notes { get; set; }
}
