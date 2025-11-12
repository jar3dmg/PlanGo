namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa un viaje obtenido desde la API.
/// </summary>
public class TripGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Destination { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Budget { get; set; }
    public string Notes { get; set; } = string.Empty;
}
