namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa una actividad registrada dentro de un viaje.
/// </summary>
public class ActivityGetDto
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal Cost { get; set; }
    public string? Notes { get; set; }
}
