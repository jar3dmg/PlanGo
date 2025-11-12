namespace TravelOrganizer.Client.Models;

/// <summary>
/// Representa los datos necesarios para crear o editar una actividad.
/// </summary>
public class ActivityPostDto
{
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDateTime { get; set; } = DateTime.Now;
    public DateTime EndDateTime { get; set; } = DateTime.Now.AddHours(1);
    public decimal Cost { get; set; }
    public string? Notes { get; set; }
}
